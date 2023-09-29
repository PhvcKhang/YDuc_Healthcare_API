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
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace HealthCareApplication.Services;

public class PersonService : IPersonService
{
    #region Properties & Constructor
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    #endregion Properties & Constructor

    #region Person
    public async Task<Person> GetPerson(string personId)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        return person;
    }
    public async Task<bool> CreatePerson(CreatePersonViewModel viewModel)
    {

        var person = _mapper.Map<CreatePersonViewModel, Person>(viewModel);
        await _personRepository.Add(person);

        return await _unitOfWork.CompleteAsync();
    }

    public async Task<bool> UpdatePerson(string personId, UpdatePersonViewModel viewModel)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);

        person.Update(viewModel.Name, viewModel.Age, viewModel.Address, viewModel.PersonType, viewModel.Weight, viewModel.Height, viewModel.PhoneNumber, viewModel.Gender);
        _personRepository.Update(person);

        return await _unitOfWork.CompleteAsync();
    }

    public async Task<bool> DeletePerson(string personId)
    {
        await _personRepository.DeleteAsync(personId);
        return await _unitOfWork.CompleteAsync();
    }


    public async Task<bool> RemoveRelationship(string personId, string patientId)
    {
        await _personRepository.RemoveRelationshipAsync(personId,patientId);
        return await _unitOfWork.CompleteAsync();
    }

    #endregion Person

    #region Patients
    public async Task<List<PatientsViewModel>> GetAllPatients()
    {
        var patients = await _personRepository.GetAllAsync() ?? throw new ResourceNotFoundException();
        List<PatientsViewModel> patientsView = _mapper.Map<List<Person>, List<PatientsViewModel>>(patients);
        return patientsView;
    }
    public async Task<PatientInfoViewModel> GetPatientInfo(string patientId)
    {
        //listOfPeople = {patient, doctor, relative1, relative2 } respectively
        var listOfPeople = await _personRepository.GetPatientInfoAsync(patientId);
        var patientViewModel = _mapper.Map<PatientInfoViewModel>(listOfPeople[0]);

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
    public async Task<Credential> AddRelative(AddNewRelativeViewModel addNewRelativeViewModel, string patientId)
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
            await CreateRelationship(relative.PersonId, patientId);
            return new Credential() { Id = relative.PersonId };
        }
        else
        {
            //Create new relative
            var relative = _mapper.Map<AddNewRelativeViewModel, Person>(addNewRelativeViewModel);
            var entity = await _personRepository.Add(relative);
            await _unitOfWork.CompleteAsync();

            //Create relationship between patient and relative
            await CreateRelationship(relative.PersonId, patientId);
            return new Credential() { Id = entity.PersonId };
        }
    }
    public async Task<List<Person>> GetRelativesByPatientId(string patientId)
    {
        var relatives = await _personRepository.GetRelativesByPatientIdAsync(patientId);
        return relatives;
    }
    #endregion Patients

    #region Doctor
    public async Task<DoctorInfoViewModel> GetDoctorInfo(string doctorId)
    {
        var doctor = await _personRepository.GetDoctorInfoAsync(doctorId) ?? throw new ResourceNotFoundException(nameof(Person), doctorId);
        return _mapper.Map<DoctorInfoViewModel>(doctor);
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

    public async Task<Credential> AddNewPatient(AddNewPatientViewModel addNewPatientViewModel, string doctorId)
    {
        //Check if the relative has existed
        var isExisting = await _personRepository.IsExisting(addNewPatientViewModel.PhoneNumber);
        if (isExisting is true)
        {
            throw new Exception("This phone number has been registed by another person");
        }

        //Create new relative
        var patient = _mapper.Map<AddNewPatientViewModel, Person>(addNewPatientViewModel);
        var entity = await _personRepository.Add(patient);
        await _unitOfWork.CompleteAsync();

        //Create relationship between patient and relative
        await CreateRelationship(doctorId, patient.PersonId);

        return new Credential() { Id = entity.PersonId };
    }
    public async Task<bool> DeletePatientById(string patientId)
    {
        List<Person> relatives = await _personRepository.GetRelativesByPatientIdAsync(patientId);

        foreach (Person relative in relatives)
        {
            await _personRepository.RemoveRelationshipAsync(relative.PersonId, patientId);
            await _personRepository.DeleteAsync(relative.PersonId);
        }
        await _personRepository.DeleteAsync(patientId);
        return await _unitOfWork.CompleteAsync();
    }
    #endregion Doctor

    #region Relative
    public async Task<List<RelativesViewModel>> GetAllRelatives()
    {
        var relatives = await _personRepository.GetAllRelativesAsync() ?? throw new ResourceNotFoundException();
        return _mapper.Map<List<Person>,List<RelativesViewModel>>(relatives);
    }
    public async Task<RelativeInfoViewModel> GetRelativeById(string relativeId)
    {
        var relative = await _personRepository.GetRelativeAsync(relativeId) ?? throw new ResourceNotFoundException(nameof(Person), relativeId);
        return _mapper.Map<RelativeInfoViewModel>(relative);
    }



    #endregion Relative

}
