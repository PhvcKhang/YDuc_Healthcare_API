using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Relatives;
using HealthCareApplication.Services;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("[controller]")]
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
    [Route("{patientId}")]
    public async Task<IActionResult> CreateBloodSugar([FromRoute] string patientId, [FromBody] CreateBloodSugarViewModel bloodSugar)
    {
        try
        {
            //Create a new statistic
            BloodSugar newBloodSugar = await _bloodSugarService.CreateBloodSugar(patientId, bloodSugar);
            var updatedDate = DateTime.Now.ToString();

            //Look for the doctor who responsibilizes for this patient 
            Person doctor = await _personService.FindDoctorByPatientId(patientId);
            List<Person> relatives = await _personService.GetRelativesByPatientId(patientId);

            //Push Notification to Doctor
            Person patient = await _personService.GetPerson(patientId);

            //Push Notificaiton to OneSignal
            var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
            var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số đường huyết";
            var ENcontent = "Patient " + patient.Name + " has just updated " + pronounce + " blood sugar readings";
            ENotificationType notificationType = ENotificationType.BloodSugar;

            var notifications = await _notificationHelper.PushAsync(patient, doctor, relatives, VIcontent, ENcontent, notificationType, bloodSugar: newBloodSugar);

            //Add user-defined sample of this notification to database
            foreach (Notification notification in notifications)
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
    public async Task<BloodSugarViewModel> GetNewestBloodPressure()
    {
        return await _bloodSugarService.GetNewestAsync();
    }

    [HttpGet]
    [Route("{patientId}")]
    public async Task<List<BloodSugarViewModel>> GetBloodSugars([FromRoute] string patientId, [FromQuery] TimeQuery timeQuery)
    {
        return await _bloodSugarService.GetBloodSugars(patientId, timeQuery);
    }
}
