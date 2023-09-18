using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
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
    [Route("{personId}")] //P107982209863 - D057298409432
    public async Task<IActionResult> CreateBloodPressure([FromRoute] string personId, [FromBody] CreateBloodPressureViewModel bloodPressure)
    {
        try
        {

            //Create a new statistic
            var newBloodPressure = await _bloodPressureService.CreateBloodPressure(personId, bloodPressure);
            var updatedDate = DateTime.Now.ToString();

            //Look for the doctor who responsibilizes for this patient 
            Person doctor = await _personService.FindDoctorByPatientId(personId);

            //Look for the patient
            PersonViewModel patient = await _personService.GetPerson(personId);


            //Push Notificaiton to OneSignal
            var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
            var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số huyết áp";
            var ENcontent = "Patient " + patient.Name + " has just updated "+pronounce+" blood pressure readings";

            ENotificationType notificationType = ENotificationType.BloodPressure;

            var notification =  await _notificationHelper.PushAsync(personId, doctor,patient.Name, VIcontent, ENcontent, notificationType, bloodPressure: newBloodPressure);

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
