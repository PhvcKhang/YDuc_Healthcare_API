using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Domains.Repositories;

public interface IPersonRepository
{
    #region Person
    public Task<Person> Add(Person person);
    public Task<Person?> GetAsync(string personId);
    public Task<List<Person>> GetListByIdAsync(List<string> personIds);
    public Person Update(Person person);
    public Task DeleteAsync(string personId);
    public Task<Person?> GetByPhoneNumber(string phoneNumber);
    public Task<Person?> GetPersonWithPatientsAsync(string personId);
    public Task<List<Person>?> GetPatientsByIdAsync(string personId);
    public Task<Person> RemoveRelationshipAsync(string personId, string patientId);
    #endregion Person

    #region Patient
    public Task<List<Person>> GetAllAsync();
    public Task<List<Person>> GetPatientInfoAsync(string personId);
    public Task<int> GetTheNumberOfRelatives(Person patient);
    public Task<Person?> GetRelativeAsync(string patientId);
    public Task<List<Person>> GetRelativesByPatientIdAsync(string patientId);
    public Person CreateRelationshipAsync(Person person, Person patient);
    #endregion Patient

    #region Doctor
    public Task<Person?> FindByIdAsync(string patientId);
    public Task<Person> AddPatientAsync(string personId, string patientId);
    public Task<Person?> GetDoctorInfoAsync(string doctorId);
    public Task<List<Person>?> GetAllDoctorsAsync();
    #endregion Doctor

    #region Relative
    public Task<List<Person>?> GetAllRelativesAsync();

    #endregion Relative
    #region Functions
    public Task<bool> IsExisting(string phoneNumber);
    #endregion Functions



}
