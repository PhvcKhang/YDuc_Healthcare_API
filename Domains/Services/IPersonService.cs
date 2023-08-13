using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Domains.Services;

public interface IPersonService
{
    public Task<PersonViewModel> GetPerson(string personId);
    public Task<bool> CreatePerson(CreatePersonViewModel viewModel);
    public Task<bool> UpdatePerson(string personId, UpdatePersonViewModel viewModel);
    public Task<bool> DeletePerson(string personId);
}
