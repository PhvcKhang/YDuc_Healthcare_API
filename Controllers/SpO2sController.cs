using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Relatives;
using HealthCareApplication.Resource.SpO2;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpO2sController : Controller
    {
        private readonly ISpO2Service _spO2Service;
        private readonly INotificationService _notificationService;
        private readonly IPersonService _personService;
        private readonly NotificationHelper _notificationHelper;

        public SpO2sController(ISpO2Service spO2Service, INotificationService notificationService, IPersonService personService)
        {
            _spO2Service = spO2Service;
            _notificationService = notificationService;
            _personService = personService;
            _notificationHelper = new NotificationHelper();
        }

        [HttpGet]
        [Route("{patientId}")]
        public async Task<List<SpO2ViewModel>> GetAll([FromRoute] string patientId, [FromQuery] TimeQuery timeQuery )
        {
            return await _spO2Service.GetAll(patientId, timeQuery);    
        }
        [HttpPost]
        [Route("{patientId}")]
        public async Task<IActionResult> CreateSpO2([FromRoute] string patientId, [FromBody] CreateSpO2ViewModel spO2)
        {
            try
            {

                //Create a new statistic
                var newSpO2 = await _spO2Service.CreateSpO2(patientId, spO2);
                var updatedDate = DateTime.Now.ToString();

                //Look for the doctor who responsibilizes for this patient 
                Person doctor = await _personService.FindDoctorByPatientId(patientId);
                List<Person> relatives = await _personService.GetRelativesByPatientId(patientId);

                //Look for the patient
                Person patient = await _personService.GetPerson(patientId);


                //Push Notificaiton to OneSignal
                var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
                var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số SpO2";
                var ENcontent = "Patient " + patient.Name + " has just updated " + pronounce + " SpO2 readings";

                ENotificationType notificationType = ENotificationType.SpO2;

                var notifications = await _notificationHelper.PushAsync(patient, doctor, relatives, VIcontent, ENcontent, notificationType, spO2: newSpO2);

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
    }
}
