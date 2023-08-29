using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;

namespace HealthCareApplication.Domains.Services;

public interface IPersonService
{
    #region Person
    public Task<PersonViewModel> GetPerson(string personId);
    public Task<bool> CreatePerson(CreatePersonViewModel viewModel);
    public Task<bool> UpdatePerson(string personId, UpdatePersonViewModel viewModel);
    public Task<bool> DeletePerson(string personId);
    #endregion Person

    #region Patient
    public Task<List<PatientsViewModel>> GetAllPatients();
    public Task<PatientInfoViewModel> GetPatientInfo(string patientId);
    #endregion Patient

    #region Doctor
    public Task<DoctorInfoViewModel> GetDoctorInfo(string doctorId);
    public Task<List<DoctorsViewModel>?> GetAllDoctors();
    public Task<bool> AddPatientById(string doctorId,string patientId);

    public Task<Person> FindDoctorByPatientId(string patientId);
    #endregion Doctor
}
