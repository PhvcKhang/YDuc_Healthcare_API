using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Resource.SpO2;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Domains.Services
{
    public interface ISpO2Service
    {
        Task<List<SpO2ViewModel>> GetAll(string personId, TimeQuery timeQuery);
        Task<SpO2> CreateSpO2(string personId, CreateSpO2ViewModel spO2);
    }
}
