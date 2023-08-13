using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories;

public interface IBodyTemperatureRepository
{
    public Task<BodyTemperature> Add(BodyTemperature bodyTemperature);
    public Task<BodyTemperature?> GetAsync(string bodyTemperatureId);
    public Task<BodyTemperature?> GetNewestAsync();
    public Task<List<BodyTemperature>> GetListByTimeQueryAsync(string personId, TimeQuery timeQuery);
    public Task<bool> ExistsAsync(string bodyTemperatureId);
}
