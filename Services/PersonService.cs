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
    public async Task<bool> AddNewRelative(AddNewRelativeViewModel addNewRelativeViewModel, string patientId)
    {
        //Check the number of relatives
        List<Person> relatives = await _personRepository.GetRelativesByPatientIdAsync(patientId);
        var count = relatives.Count();
        if(count >= 2)
        {
            throw new Exception("The number of relatives belonging to this patient is already maxium");
        }

        //Check if the relative has existed
        var isExisting = await _personRepository.IsExisting(addNewRelativeViewModel.PhoneNumber);
        if (isExisting is true)
        {
            throw new EntityDuplicationException("This phone number has been registed by another person");
        }
        //Look for patient entity
        var relative = _mapper.Map<AddNewRelativeViewModel, Person>(addNewRelativeViewModel);

        //Create new relative
        await _personRepository.Add(relative);
        await _unitOfWork.CompleteAsync();

        //Create relationship between patient and relative
        return await CreateRelationshipAsync(relative.PersonId, patientId);
    }
    public async Task<bool> AddExistingRelative(string relativePhoneNumber, string patientId)
    {
        //Check the number of relatives
        List<Person> relatives = await _personRepository.GetRelativesByPatientIdAsync(patientId);
        var count = relatives.Count();
        if (count >= 2)
        {
            throw new Exception("The number of relatives belonging to this patient is already maxium");
        }

        //Check if the relative has existed
        var isExisting = await _personRepository.IsExisting(relativePhoneNumber);
        if (isExisting is false)
        {
            throw new Exception("The phone number hasn't been registed yet");
        }
        //Create relationship between patient and relative
        var relative = await _personRepository.GetByPhoneNumber(relativePhoneNumber) ?? throw new ResourceNotFoundException("Can not find the entity");
        return await CreateRelationshipAsync(relative.PersonId, patientId);
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

    public async Task<bool> CreateRelationshipAsync(string personId, string patientId)
    {
        var person = await _personRepository.GetPersonWithPatientsAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        var patient = await _personRepository.GetAsync(patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        var theNumberOfRelatives = await _personRepository.GetTheNumberOfRelatives(patient);

        if (theNumberOfRelatives >= 2)
            {
                throw new Exception("The number of relatives belonging to this patient is already maxium");
            }
        if (person.PersonType == EPersonType.Relative)
        {
            if (person.Patients.Contains(patient))
            {
                throw new Exception("The relationship between these entities has been existed");
            }
        }

        _personRepository.CreateRelationship(person,patient);

        return await _unitOfWork.CompleteAsync();
    }
    public async Task<Person> FindDoctorByPatientId(string patientId)
    {
        return await _personRepository.FindByIdAsync(patientId) ?? throw new ResourceNotFoundException($"Can not find out the doctor who is responsible for patient with ID: {patientId}"); 
    }

    public async Task<Credential> AddNewPatient(AddNewPatientViewModel addNewPatientViewModel, string doctorId)
    {
        var patient = _mapper.Map<AddNewPatientViewModel,Person>(addNewPatientViewModel);

        //Create new relative
        await _personRepository.Add(patient);
        await _unitOfWork.CompleteAsync();

        //Create relationship between patient and relative
        await _personRepository.AddPatientAsync(doctorId, patient.PersonId);
        await _unitOfWork.CompleteAsync();

        //Create Username and Password
        var username = patient.PhoneNumber;
        var password = patient.PhoneNumber;

        Credential credential = new (username, password);

        return credential;
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
