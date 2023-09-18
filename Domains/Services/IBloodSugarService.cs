using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;

namespace HealthCareApplication.Domains.Services;

public interface IBloodSugarService
{
    public Task<BloodSugarViewModel> GetNewestAsync();
    public Task<List<BloodSugarViewModel>> GetBloodSugars(string personId, TimeQuery timeQuery);
    public Task<BloodSugar> CreateBloodSugar(string personId, CreateBloodSugarViewModel viewModel);
}
