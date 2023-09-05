using AutoMapper;
using Azure.Core;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Relatives;
using System.Collections.Generic;


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
    public async Task<PersonViewModel> GetPerson(string personId)
    {
        var person = await _personRepository.GetAsync(personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        return _mapper.Map<PersonViewModel>(person);
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
        var address = _mapper.Map<Address>(viewModel.Address);

        person.Update(viewModel.Name, viewModel.Age, viewModel.PersonType, address, viewModel.Weight, viewModel.Height, viewModel.PhoneNumber, viewModel.Gender);
        _personRepository.Update(person);

        return await _unitOfWork.CompleteAsync();
    }

    public async Task<bool> DeletePerson(string personId)
    {
        await _personRepository.DeleteAsync(personId);
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
        var patient = await _personRepository.GetPatientInfoAsync(patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        patient.BloodPressures.RemoveAll(x => x != patient.BloodPressures.FirstOrDefault());
        patient.BloodSugars.RemoveAll(x => x != patient.BloodSugars.FirstOrDefault());
        patient.BodyTemperatures.RemoveAll(x => x != patient.BodyTemperatures.FirstOrDefault());
        return _mapper.Map<PatientInfoViewModel>(patient);
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

    public async Task<bool> AddPatientById(string personId, string patientId)
    {

        await _personRepository.AddPatient(personId,patientId);

        return await _unitOfWork.CompleteAsync();
    }
    public async Task<Person> FindDoctorByPatientId(string patientId)
    {
        return await _personRepository.FindByIdAsync(patientId) ?? throw new ResourceNotFoundException(); 
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
