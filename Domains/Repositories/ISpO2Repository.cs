using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories
{
    public interface ISpO2Repository
    {
        public Task<SpO2> Add(SpO2 spO2);
    }
}
