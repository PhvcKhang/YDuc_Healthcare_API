using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Resource.BloodPressure;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BloodPressuresController : Controller
{
    private readonly IBloodPressureService _bloodPressureService;

    public BloodPressuresController(IBloodPressureService bloodPressureService)
    {
        _bloodPressureService = bloodPressureService;
    }

    [HttpPost]
    [Route("{personId}")]
    public async Task<IActionResult> CreateBloodPressure([FromRoute] string personId, [FromBody] CreateBloodPressureViewModel bloodPressure)
    {
        try
        {
            var result = await _bloodPressureService.CreateBloodPressure(personId, bloodPressure);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new ErrorMessage(ex);
            return BadRequest(errorMessage);
        }
    }

    [HttpGet]
    [Route("Newest")]
    public async Task<BloodPressureViewModel> GetNewestBloodPressure()
    {
        return await _bloodPressureService.GetNewestAsync();
    }

    [HttpGet]
    [Route("{personId}")]
    public async Task<List<BloodPressureViewModel>> GetBloodPressures([FromRoute] string personId, [FromQuery] TimeQuery timeQuery)
    {
        return await _bloodPressureService.GetBloodPressures(personId, timeQuery);
    }
}
