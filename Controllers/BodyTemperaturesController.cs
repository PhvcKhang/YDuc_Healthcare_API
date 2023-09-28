using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Relatives;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BodyTemperaturesController : Controller
{
    private readonly IBodyTemperatureService _bodyTemperatureService;
    private readonly INotificationService _notificationService;
    private readonly IPersonService _personService;
    private readonly NotificationHelper _notificationHelper;
    public BodyTemperaturesController(IBodyTemperatureService bodyTemperatureService, INotificationService notificationService, IPersonService personService)
    {
        _bodyTemperatureService = bodyTemperatureService;
        _notificationService = notificationService;
        _personService = personService;
        _notificationHelper = new NotificationHelper();
    }

    [HttpPost]
    [Route("{personId}")]
    public async Task<IActionResult> CreateBodyTemperature([FromRoute] string personId, [FromBody] CreateBodyTemperatureViewModel bodyTemperature)
    {
        try
        {
            //Create a new statistic
            var newBodyTemperature = await _bodyTemperatureService.CreateBodyTemperature(personId, bodyTemperature);
            var updatedDate = DateTime.Now.ToString();

            //Look for the doctor who responsibilizes for this patient 
            Person doctor = await _personService.FindDoctorByPatientId(personId);
            List<Person> relatives = await _personService.GetRelativesByPatientId(personId);

            //Push Notification to Doctor
            Person patient = await _personService.GetPerson(personId);

            //Push Notificaiton to OneSignal
            var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
            var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số thân nhiệt";
            var ENcontent = "Patient " + patient.Name + " has just updated " + pronounce + " body temperature readings";
            ENotificationType notificationType = ENotificationType.BodyTemperature;

            var notification = await _notificationHelper.PushAsync(patient, doctor, relatives, VIcontent, ENcontent,notificationType, bodyTemperature: newBodyTemperature);

            //Add user-defined sample of this notification to database
            await _notificationService.CreateNotification(notification);

            return Ok(true);
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
