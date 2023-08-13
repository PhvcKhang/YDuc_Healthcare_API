using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories;

public interface IBloodPressureRepository
{
    public Task<BloodPressure> Add(BloodPressure bloodPressure);
    public Task<BloodPressure?> GetAsync(string bloodPressureId);
    public Task<BloodPressure?> GetNewestAsync();
    public Task<List<BloodPressure>> GetListByTimeQueryAsync(string personId, TimeQuery timeQuery);
    public Task<bool> ExistsAsync(string bloodPressureId);
}
