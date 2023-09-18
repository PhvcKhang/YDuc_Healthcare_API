﻿using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.SpO2;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApplication.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost]
        [Route("{personId}")]
        public async Task<IActionResult> CreateSpO2([FromRoute] string personId, [FromBody] CreateSpO2ViewModel spO2)
        {
            try
            {

                //Create a new statistic
                var newSpO2 = await _spO2Service.CreateSpO2(personId, spO2);
                var updatedDate = DateTime.Now.ToString();

                //Look for the doctor who responsibilizes for this patient 
                Person doctor = await _personService.FindDoctorByPatientId(personId);

                //Look for the patient
                PersonViewModel patient = await _personService.GetPerson(personId);


                //Push Notificaiton to OneSignal
                var pronounce = (patient.Gender == EPersonGender.Male) ? "his" : "her";
                var VIcontent = "Bệnh nhân " + patient.Name + " vừa cập nhật chỉ số SpO2";
                var ENcontent = "Patient " + patient.Name + " has just updated " + pronounce + " SpO2 readings";

                ENotificationType notificationType = ENotificationType.SpO2;

                var notification = await _notificationHelper.PushAsync(personId, doctor, patient.Name, VIcontent, ENcontent, notificationType, spO2: newSpO2);

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
    }
}
