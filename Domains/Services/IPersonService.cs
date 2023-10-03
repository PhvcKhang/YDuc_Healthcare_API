using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Patients;
using HealthCareApplication.Resource.Persons.Relatives;
using HealthCareApplication.Resource.Users;

namespace HealthCareApplication.Domains.Services;

public interface IPersonService
{
    #region Admin
    public Task<string> CreateDoctorAccount(DoctorRegistrationViewModel registrationModel);
    public Task<bool> DeleteDoctorAccount(string doctorId);
    public Task<Person> GetPerson(string personId);

    #endregion Admin

    #region User
    public Task<bool> ChangePassword(string userId, string currentPassword, string newPassword);
    public Task<bool> UpdateProfile(UpdateProfileViewModel model,string userId);
    #endregion User

    #region Patient
    public Task<List<PatientsViewModel>> GetAllPatients();
    public Task<PatientProfileViewModel> GetPatientInfo(string patientId);
    public Task<List<Person>> GetRelativesByPatientId(string patientId);
    public Task<string> AddRelative(AddNewRelativeViewModel addNewRelativeViewModel, string patientId);
    #endregion Patient

    #region Doctor
    public Task<DoctorIProfileViewModel> GetDoctorInfo(string doctorId);
    public Task<List<DoctorsViewModel>?> GetAllDoctors();
    public Task<Person> FindDoctorByPatientId(string patientId);
    public Task<string> AddNewPatient(AddNewPatientViewModel addNewPatientViewModel, string doctorId);
    public Task<bool> DeletePatientById(string patientId);
    #endregion Doctor

    #region Relative
    public Task<List<RelativesViewModel>> GetAllRelatives();
    public Task<RelativeProfileViewModel> GetRelativeById(string relativeId);

    #endregion Relative
}
