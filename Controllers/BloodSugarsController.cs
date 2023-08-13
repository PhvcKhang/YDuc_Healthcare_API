using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Resource.BloodSugar;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BloodSugarsController : Controller
{
    private readonly IBloodSugarService _bloodSugarService;

    public BloodSugarsController(IBloodSugarService bloodSugarService)
    {
        _bloodSugarService = bloodSugarService;
    }

    [HttpPost]
    [Route("{personId}")]
    public async Task<IActionResult> CreateBloodSugar([FromRoute] string personId, [FromBody] CreateBloodSugarViewModel bloodSugar)
    {
        try
        {
            var result = await _bloodSugarService.CreateBloodSugar(personId, bloodSugar);
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
    public async Task<BloodSugarViewModel> GetNewestBloodPressure()
    {
        return await _bloodSugarService.GetNewestAsync();
    }

    [HttpGet]
    [Route("{personId}")]
    public async Task<List<BloodSugarViewModel>> GetBloodSugars([FromRoute] string personId, [FromQuery] TimeQuery timeQuery)
    {
        return await _bloodSugarService.GetBloodSugars(personId, timeQuery);
    }
}
