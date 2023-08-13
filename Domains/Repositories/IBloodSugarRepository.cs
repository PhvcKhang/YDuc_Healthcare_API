using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories;

public interface IBloodSugarRepository
{
    public Task<BloodSugar> Add(BloodSugar bloodSugar);
    public Task<BloodSugar?> GetAsync(string bloodSugarId);
    public Task<BloodSugar?> GetNewestAsync();
    public Task<List<BloodSugar>> GetListByTimeQueryAsync(string personId, TimeQuery timeQuery);
    public Task<bool> ExistsAsync(string bloodSugarId);
}
