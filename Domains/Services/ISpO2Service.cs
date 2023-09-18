using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.SpO2;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Domains.Services
{
    public interface ISpO2Service
    {
        Task<SpO2> CreateSpO2(string personId, CreateSpO2ViewModel spO2);
    }
}
