using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;

namespace HealthCareApplication.Domains.Repositories
{
    public interface ISpO2Repository
    {
        public Task<SpO2> Add(SpO2 spO2);
        public Task<List<SpO2>> GetAllAsync(string personId, TimeQuery timeQuery);
    }
}
