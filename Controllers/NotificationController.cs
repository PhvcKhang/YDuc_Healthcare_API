using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.Notification;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace HealthCareApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly NotificationHelper _notificaitonHelper;
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificaitonHelper = new NotificationHelper();
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("All")]
        public async Task<List<NotificationViewModel>> GetAll()
        {
            return await _notificationService.GetAll();
        }
        [HttpGet]
        [Route("{doctorId}")]
        public async Task<List<NotificationViewModel>> GetByDoctorId([FromRoute] string doctorId)
        {
            return await _notificationService.GetByDoctorId(doctorId);
        }
        [HttpPut]
        [Route("{notificationId}/seen")]
        public async Task<IActionResult> ChangeStatus( [FromRoute] string notificationId)
        {
            try
            {
                return Ok(await _notificationService.ChangeStatus(notificationId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         } 
    }
}
