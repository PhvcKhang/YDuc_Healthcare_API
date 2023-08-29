using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Domains.Repositories;

public interface IPersonRepository
{
    public Task<Person> Add(Person person);
    public Task<Person?> GetAsync(string personId);
    public Task<List<Person>> GetAllAsync();
    public Task<Person?> GetPatientInfoAsync(string personId);
    public Task<List<Person>> GetListByIdAsync(List<string> personIds);
    public Person Update(Person person);
    public Task<bool> ExistsAsync(string personId);
    public Task DeleteAsync(string personId);
    public Task<Person?> GetDoctorInfoAsync(string doctorId);
    public Task<List<Person>?> GetAllDoctorsAsync();
    public Task<Person> AddPatient(string doctorId, string patientId);
    public Task<Person?> FindByIdAsync(string patientId);
}
