using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Patients;
using HealthCareApplication.Resource.Persons.Relatives;

namespace HealthCareApplication.Domains.Services;

public interface IPersonService
{
    #region Person
    public Task<PersonViewModel> GetPerson(string personId);
    public Task<bool> CreatePerson(CreatePersonViewModel viewModel);
    public Task<bool> UpdatePerson(string personId, UpdatePersonViewModel viewModel);
    public Task<bool> DeletePerson(string personId);
    public Task<bool> RemoveRelationship(string personId, string patientId);
    #endregion Person

    #region Patient
    public Task<List<PatientsViewModel>> GetAllPatients();
    public Task<PatientInfoViewModel> GetPatientInfo(string patientId);
    public Task<bool> AddNewRelative(AddNewRelativeViewModel addNewRelativeViewModel, string patientId);
    public Task<bool> AddExistingRelative(string relativePhoneNumber, string patientId);
    #endregion Patient

    #region Doctor
    public Task<DoctorInfoViewModel> GetDoctorInfo(string doctorId);
    public Task<List<DoctorsViewModel>?> GetAllDoctors();
    public Task<Person> FindDoctorByPatientId(string patientId);
    public Task<Credential> AddNewPatient(AddNewPatientViewModel addNewPatientViewModel, string doctorId);
    #endregion Doctor

    #region Relative
    public Task<List<RelativesViewModel>> GetAllRelatives();
    public Task<RelativeInfoViewModel> GetRelativeById(string relativeId);

    #endregion Relative
}
