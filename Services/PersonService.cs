using AutoMapper;
using Azure.Core;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Patients;
using HealthCareApplication.Resource.Persons.Relatives;
using HealthCareApplication.Resource.Users;
using HealthCareApplication.Resource.Users.Admin;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace HealthCareApplication.Services;

public class PersonService : IPersonService
{
    #region Properties & Constructor
    private readonly UserManager<Person> _userManager;
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonService(UserManager<Person> userManager, IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userManager = userManager;
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    #endregion Properties & Constructor

    #region Admin
    public async Task<string> CreateAdminAccount(AdminRegistrationViewModel registrationModel)
    {
        var admin = _mapper.Map<Person>(registrationModel);
        await _userManager.CreateAsync(admin, registrationModel.Password);
        await _userManager.AddToRoleAsync(admin, registrationModel.PersonType.ToString().ToLowerInvariant());
        return admin.Id;
    }
    public async Task<AdminViewModel> GetAdmin(string adminId)
    {
        var admin = await _userManager.FindByIdAsync(adminId);
        var doctors = await GetAllDoctors();
        var viewModel = _mapper.Map<AdminViewModel>(admin);
        viewModel.Doctors = doctors;
        return viewModel;

    } 
    public async Task<string> CreateDoctorAccount(DoctorRegistrationViewModel registrationModel)
    {
        var userIdentity = _mapper.Map<Person>(registrationModel);

        var IsExisting = await _personRepository.GetByPhoneNumber(userIdentity.PhoneNumber);
        if(IsExisting is not null)
        {
            throw new EntityDuplicationException("This phone number has been already registered by another account");
        }

        await _userManager.CreateAsync(userIdentity, registrationModel.Password);

        await _userManager.AddToRoleAsync(userIdentity, registrationModel.PersonType.ToString().ToLowerInvariant());

        return userIdentity.Id;
    }
    public async Task<bool> DeleteDoctorAccount(string doctorId)
    {

        var doctor = await _personRepository.GetPersonWithPatientsAsync(doctorId) ?? throw new ResourceNotFoundException(nameof(Person), doctorId);

        List<Person> patients = new();
        patients.AddRange(doctor.Patients);

        foreach (var patient in patients)
        {
            var relatives = await _personRepository.GetRelativesByPatientIdAsync(patient.Id);
            foreach (var relative in relatives)
            {
                await _personRepository.RemoveRelationshipAsync(relative.Id, patient.Id);
                await _userManager.DeleteAsync(relative);
            }
            await _personRepository.RemoveRelationshipAsync(doctor.Id, patient.Id);
            await _userManager.DeleteAsync(patient);
        }

        await _userManager.DeleteAsync(doctor);

        return await _unitOfWork.CompleteAsync();
        
    }
    public async Task<Person> GetPerson(string personId)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        return person;
    }

    #endregion Admin

    #region User
    public async Task<bool> ChangePassword(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new ResourceNotFoundException(nameof(Person), userId); ;

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if(result.Succeeded is not true)
        {
            throw new InvalidOperationException("Password is invalid");
        }
        return result.Succeeded;
    }
    public async Task<bool> UpdateProfile(UpdateProfileViewModel model, string userId)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userId);
        userToUpdate.Update(model.Name, model.Age, model.Address, model.Weight, model.Height, model.Gender);
        await _userManager.UpdateAsync(userToUpdate);

        return await _unitOfWork.CompleteAsync();
    }
    #endregion User

    #region Patients
    public async Task<List<PatientsViewModel>> GetAllPatients()
    {
        var patients = await _personRepository.GetAllAsync() ?? throw new ResourceNotFoundException();
        List<PatientsViewModel> patientsView = _mapper.Map<List<Person>, List<PatientsViewModel>>(patients);
        return patientsView;
    }
    public async Task<PatientProfileViewModel> GetPatientInfo(string patientId)
    {
        //listOfPeople = {patient, doctor, relative1, relative2 } respectively
        var listOfPeople = await _personRepository.GetPatientInfoAsync(patientId);
        var patientViewModel = _mapper.Map<PatientProfileViewModel>(listOfPeople[0]);

        //Demo-Only Relationship error
        if (listOfPeople.Count() == 4)
        {
            patientViewModel.Doctor = _mapper.Map<DoctorsViewModel>(listOfPeople[1]);
            patientViewModel.Relatives = _mapper.Map<List<Person>, List<RelativesViewModel>>(new List<Person>() { listOfPeople[2], listOfPeople[3] });
        }
        else if (listOfPeople.Count() == 3)
        {
            patientViewModel.Doctor = _mapper.Map<DoctorsViewModel>(listOfPeople[1]);
            patientViewModel.Relatives = _mapper.Map<List<Person>, List<RelativesViewModel>>(new List<Person>() { listOfPeople[2] });
        }
        else if (listOfPeople.Count() == 2)
        {
            patientViewModel.Doctor = _mapper.Map<DoctorsViewModel>(listOfPeople[1]);
        }

        return patientViewModel;
    }
    public async Task<string> AddRelative(AddNewRelativeViewModel addNewRelativeViewModel, string patientId)
    {
        //Check the number of relatives
        List<Person> relatives = await _personRepository.GetRelativesByPatientIdAsync(patientId);
        var count = relatives.Count();
        if(count >= 2)
        {
            throw new Exception("The number of relatives belonging to this patient is already maxium");
        }

        //Check if the relative has existed
        var isAccountExisting = await _personRepository.IsExisting(addNewRelativeViewModel.PhoneNumber);

        if (isAccountExisting is true)
        {
            //Look for the relative
            var relative = await _personRepository.GetByPhoneNumber(addNewRelativeViewModel.PhoneNumber) ?? throw new Exception("Can not find the entity");
            //Create relationship between patient and relative
            await CreateRelationship(relative.Id, patientId);
            return relative.Id;
        }
        else
        {
            //Create new relative
            var relative = _mapper.Map<AddNewRelativeViewModel, Person>(addNewRelativeViewModel);
            await _userManager.CreateAsync(relative, addNewRelativeViewModel.Password);
            await _userManager.AddToRoleAsync(relative, relative.PersonType.ToString().ToLowerInvariant());
            await _unitOfWork.CompleteAsync();

            //Create relationship between patient and relative
            await CreateRelationship(relative.Id, patientId);
            return relative.Id;
        }
    }
    public async Task<List<Person>> GetRelativesByPatientId(string patientId)
    {
        var relatives = await _personRepository.GetRelativesByPatientIdAsync(patientId);
        return relatives;
    }
    #endregion Patients

    #region Doctor
    public async Task<DoctorIProfileViewModel> GetDoctorInfo(string doctorId)
    {
        var doctor = await _personRepository.GetDoctorInfoAsync(doctorId) ?? throw new ResourceNotFoundException(nameof(Person), doctorId);
        return _mapper.Map<DoctorIProfileViewModel>(doctor);
    }
    public async Task<List<DoctorsViewModel>?> GetAllDoctors()
    {
        var doctors = await _personRepository.GetAllDoctorsAsync() ?? throw new ResourceNotFoundException();
        return _mapper.Map<List<Person>?,List<DoctorsViewModel>?>(doctors);
    }

    public async Task<bool> CreateRelationship(string personId, string patientId)
    {
        var person = await _personRepository.GetPersonWithPatientsAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        var patient = await _personRepository.GetAsync(patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);

        if (person.Patients.Contains(patient))
        {
           throw new Exception("The relationship between these entities has been existed");
        }
        if(person.PersonType == EPersonType.Doctor)
        {
            var existingDoctor = await _personRepository.FindByIdAsync(patientId);
            if (existingDoctor != null)
            {
                throw new Exception("This patient has been managed by another doctor");
            }
        }
        _personRepository.CreateRelationshipAsync(person,patient);

        return await _unitOfWork.CompleteAsync();
    }
    public async Task<Person> FindDoctorByPatientId(string patientId)
    {
        return await _personRepository.FindByIdAsync(patientId) ?? throw new ResourceNotFoundException($"Can not find out the doctor who is responsible for patient with ID: {patientId}"); 
    }

    public async Task<string> AddNewPatient(AddNewPatientViewModel addNewPatientViewModel, string doctorId)
    {

        //Check if the patient and the doctor has existed 
        var isExisting = await _personRepository.IsExisting(addNewPatientViewModel.PhoneNumber);
        var patient = _mapper.Map<AddNewPatientViewModel, Person>(addNewPatientViewModel);
        var doctor = _personRepository.GetAsync(doctorId) ?? throw new ResourceNotFoundException(nameof(Person), doctorId);

        if (isExisting is true)
        {
            //Create relationship between patient and relative
            patient = await _personRepository.GetByPhoneNumber(addNewPatientViewModel.PhoneNumber) ?? throw new ResourceNotFoundException($"{patient.Id}");
            await CreateRelationship(doctorId, patient.Id);
            return patient.Id;
        }
        if(isExisting is false)
        {
            //Create new patient
            await _userManager.CreateAsync(patient, addNewPatientViewModel.Password);
            await _userManager.AddToRoleAsync(patient, addNewPatientViewModel.PersonType.ToString().ToLowerInvariant());
            await CreateRelationship(doctorId, patient.Id);

            return patient.Id;
        }

        return string.Empty;
    }
    public async Task<bool> DeletePatientById(string patientId)
    {
        var patient = await _personRepository.GetAsync(patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        var relatives = await _personRepository.GetRelativesByPatientIdAsync(patientId);

        foreach (var relative in relatives)
        {
            await _personRepository.RemoveRelationshipAsync(relative.Id, patient.Id);
            await _userManager.DeleteAsync(relative);
        }
        await _userManager.DeleteAsync(patient);
        return await _unitOfWork.CompleteAsync();
    }
    #endregion Doctor

    #region Relative
    public async Task<List<RelativesViewModel>> GetAllRelatives()
    {
        var relatives = await _personRepository.GetAllRelativesAsync() ?? throw new ResourceNotFoundException();
        return _mapper.Map<List<Person>,List<RelativesViewModel>>(relatives);
    }
    public async Task<RelativeProfileViewModel> GetRelativeById(string relativeId)
    {
        var relative = await _personRepository.GetRelativeAsync(relativeId) ?? throw new ResourceNotFoundException(nameof(Person), relativeId);
        return _mapper.Map<RelativeProfileViewModel>(relative);
    }

    public async Task<bool> DeleteRelativeAccount(string patientId, string relativeId)
    {
        var userToDelete = await _userManager.FindByIdAsync(relativeId) ?? throw new ResourceNotFoundException(nameof(Person), relativeId);

        await _personRepository.RemoveRelationshipAsync(relativeId, patientId);

        await _userManager.DeleteAsync(userToDelete);

        return await _unitOfWork.CompleteAsync();
    }



    #endregion Relative

}
