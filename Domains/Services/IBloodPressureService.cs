using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Resource.BloodPressure;

namespace HealthCareApplication.Domains.Services;

public interface IBloodPressureService
{
    public Task<BloodPressureViewModel> GetNewestAsync();
    public Task<List<BloodPressureViewModel>> GetBloodPressures(string personId, TimeQuery timeQuery);
    public Task<bool> CreateBloodPressure(string personId, CreateBloodPressureViewModel viewModel);
}
