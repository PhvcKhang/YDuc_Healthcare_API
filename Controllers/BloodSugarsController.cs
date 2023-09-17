using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Services;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BloodSugarsController : Controller
{
    private readonly IBloodSugarService _bloodSugarService;
    private readonly IPersonService _personService;
    private readonly INotificationService _notificationService;
    private readonly NotificationHelper _notificationHelper;

    public BloodSugarsController(IBloodSugarService bloodSugarService, IPersonService personService, INotificationService notificationService)
    {
        _bloodSugarService = bloodSugarService;
        _personService = personService;
        _notificationHelper = new NotificationHelper();
        _notificationService = notificationService;
    }

    [HttpPost]
    [Route("{personId}")]
    public async Task<IActionResult> CreateBloodSugar([FromRoute] string personId, [FromBody] CreateBloodSugarViewModel bloodSugar)
    {
        try
        {
            //Create a new statistic
            var result = await _bloodSugarService.CreateBloodSugar(personId, bloodSugar);
            var updatedDate = DateTime.Now.ToString();

            //Look for the doctor who responsibilizes for this patient 
            Person doctor = await _personService.FindDoctorByPatientId(personId);

            //Push Notification to Doctor
            PersonViewModel patient = await _personService.GetPerson(personId);

            //Push Notificaiton to OneSignal
            var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
            var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số đường huyết";
            var ENcontent = "Patient " + patient.Name + " has just updated " + pronounce + " blood sugar readings";
            var imageURL = bloodSugar.ImageLink ?? throw new ArgumentNullException(nameof(BloodPressure), "ImageLink isn't valid");
            var additionalData = new List<decimal>() { bloodSugar.Value };

            var notification = await _notificationHelper.PushAsync(personId, doctor, patient.Name, VIcontent, ENcontent, bloodSugar.ImageLink, additionalData, updatedDate);

            //Add user-defined sample of this notification to database
            await _notificationService.CreateNotification(notification);
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
