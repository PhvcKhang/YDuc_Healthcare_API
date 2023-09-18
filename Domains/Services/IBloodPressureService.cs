using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Resource.BloodPressure;

namespace HealthCareApplication.Domains.Services;

public interface IBloodPressureService
{
    public Task<BloodPressureViewModel> GetNewestAsync();
    public Task<List<BloodPressureViewModel>> GetBloodPressures(string personId, TimeQuery timeQuery);
    public Task<BloodPressure> CreateBloodPressure(string personId, CreateBloodPressureViewModel viewModel);
}
