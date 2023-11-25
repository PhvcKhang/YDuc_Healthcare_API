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
[Route("[controller]")]
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
    [Route("{patientId}")]
    public async Task<IActionResult> CreateBodyTemperature([FromRoute] string patientId, [FromBody] CreateBodyTemperatureViewModel bodyTemperature)
    {
        try
        {
            //Create a new statistic
            var newBodyTemperature = await _bodyTemperatureService.CreateBodyTemperature(patientId, bodyTemperature);
            var updatedDate = DateTime.Now.ToString();

            //Look for the doctor who responsibilizes for this patient 
            Person doctor = await _personService.FindDoctorByPatientId(patientId);
            List<Person> relatives = await _personService.GetRelativesByPatientId(patientId);

            //Push Notification to Doctor
            Person patient = await _personService.GetPerson(patientId);

            //Push Notificaiton to OneSignal
            var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
            var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số thân nhiệt";
            var ENcontent = "Patient " + patient.Name + " has just updated " + pronounce + " body temperature readings";
            ENotificationType notificationType = ENotificationType.BodyTemperature;

            var notifications = await _notificationHelper.PushAsync(patient, doctor, relatives, VIcontent, ENcontent,notificationType, bodyTemperature: newBodyTemperature);

            //Add user-defined sample of this notification to database
            foreach (var notification in notifications)
            {
                 await _notificationService.CreateNotification(notification);
            }
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
    [Route("{patientId}")]
    public async Task<List<BodyTemperatureViewModel>> GetBodyTemperatures([FromRoute] string patientId, [FromQuery] TimeQuery timeQuery)
    {
        return await _bodyTemperatureService.GetBodyTemperatures(patientId, timeQuery);
    }
}
