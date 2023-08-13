using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Resource.BodyTemperature;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BodyTemperaturesController : Controller
{
    private readonly IBodyTemperatureService _bodyTemperatureService;

    public BodyTemperaturesController(IBodyTemperatureService bodyTemperatureService)
    {
        _bodyTemperatureService = bodyTemperatureService;
    }

    [HttpPost]
    [Route("{personId}")]
    public async Task<IActionResult> CreateBodyTemperature([FromRoute] string personId, [FromBody] CreateBodyTemperatureViewModel bodyTemperature)
    {
        try
        {
            var result = await _bodyTemperatureService.CreateBodyTemperature(personId, bodyTemperature);
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
    public async Task<BodyTemperatureViewModel> GetNewestBloodPressure()
    {
        return await _bodyTemperatureService.GetNewestAsync();
    }

    [HttpGet]
    [Route("{personId}")]
    public async Task<List<BodyTemperatureViewModel>> GetBodyTemperatures([FromRoute] string personId, [FromQuery] TimeQuery timeQuery)
    {
        return await _bodyTemperatureService.GetBodyTemperatures(personId, timeQuery);
    }
}
