using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;

namespace HealthCareApplication.Domains.Services;

public interface IBodyTemperatureService
{
    public Task<BodyTemperatureViewModel> GetNewestAsync();
    public Task<List<BodyTemperatureViewModel>> GetBodyTemperatures(string personId, TimeQuery timeQuery);
    public Task<bool> CreateBodyTemperature(string personId, CreateBodyTemperatureViewModel viewModel);
}
