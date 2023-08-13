using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories;

public interface IPersonRepository
{
    public Task<Person> Add(Person person);
    public Task<Person?> GetAsync(string personId);
    public Task<List<Person>> GetListByIdAsync(List<string> personIds);
    public Person Update(Person person);
    public Task<bool> ExistsAsync(string personId);
    public Task DeleteAsync(string personId);
}
