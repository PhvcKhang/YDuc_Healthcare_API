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
        private readonly INotificationService _notificationService;
        private readonly NotificaitonHelper _notificaitonHelper;

        public NotificationController(INotificationService notificationService, NotificaitonHelper notificaitonHelper)
        {
            _notificationService = notificationService;
            _notificaitonHelper = notificaitonHelper;
        }

        [HttpGet]
        [Route("{personId}")]
        public async Task<List<NotificationViewModel>> GetByPersonId(string personId)
        {
            return await _notificationService.GetByPersonId(personId);
        }
        [HttpPost]
        [Route("PushNotification")]
        public async Task<IActionResult> PushNotificationById([FromBody]string doctorId)
        {
            try
            {
                return Ok(await _notificaitonHelper.PushAsync(doctorId));
            }
            catch (Exception ex)
            {
                var errorMessage = new ErrorMessage(ex);
                return BadRequest(errorMessage);
            }
        }
        [HttpGet]
        [Route("Test")]
        public async Task<string> Test()
        {
                NotificaitonHelper request = new NotificaitonHelper();
                
                return await request.GetAsync();
        }
    }
}
