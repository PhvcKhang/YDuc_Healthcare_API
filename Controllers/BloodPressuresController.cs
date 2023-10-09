using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Relatives;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace HealthCareApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class BloodPressuresController : Controller
{
    private readonly IBloodPressureService _bloodPressureService;
    private readonly INotificationService _notificationService;
    private readonly IPersonService _personService;
    private readonly NotificationHelper _notificationHelper;
    public BloodPressuresController(IBloodPressureService bloodPressureService, INotificationService notificationService, IPersonService personService)
    {
        _bloodPressureService = bloodPressureService;
        _notificationHelper = new NotificationHelper();
        _notificationService = notificationService;
        _personService = personService;
    }

    [HttpPost]
    [Route("{patientId}")] 
    public async Task<IActionResult> CreateBloodPressure([FromRoute] string patientId, [FromBody] CreateBloodPressureViewModel bloodPressure)
    {
        try
        {

            //Create a new statistic
            var newBloodPressure = await _bloodPressureService.CreateBloodPressure(patientId, bloodPressure);
            var updatedDate = DateTime.Now.ToString();

            //Look for the doctor, relatives who responsibilizes for this patient 
            Person doctor = await _personService.FindDoctorByPatientId(patientId);
            List<Person> relatives = await _personService.GetRelativesByPatientId(patientId);

            //Look for the patient
            Person patient = await _personService.GetPerson(patientId);

            //Push Notificaiton to OneSignal
            var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
            var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số huyết áp";
            var ENcontent = "Patient " + patient.Name + " has just updated "+pronounce+" blood pressure readings";

            ENotificationType notificationType = ENotificationType.BloodPressure;

            var notifications =  await _notificationHelper.PushAsync(patient, doctor, relatives, VIcontent, ENcontent, notificationType, bloodPressure: newBloodPressure);

            //Add this notification to our database
            foreach(Notification notification in notifications)
            {
                await _notificationService.CreateNotification(notification);

            }
            return new OkObjectResult($"{notifications.Count} notifications have just been sent");
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
    [Route("{patientId}")]
    public async Task<List<BloodPressureViewModel>> GetBloodPressures([FromRoute] string patientId, [FromQuery] TimeQuery timeQuery)
    {
        return await _bloodPressureService.GetBloodPressures(patientId, timeQuery);
    }
}
