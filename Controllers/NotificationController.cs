using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
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
        //[HttpPost]
        //[Route("PushNotification")]
        //public async Task<bool> PushNotificationById(string doctorId, string patientId)
        //{
        //   return await _notificaitonHelper.PushAsync(patientId);
        //}
        [HttpGet]
        [Route("{notificationId}")]
        public async Task<string> GetById([FromRoute] string notificationId)
        {
              return await _notificaitonHelper.GetByIdAsync(notificationId);
        }
        [HttpGet]
        [Route("All")]
        public async Task<List<Notification>> GetAll()
        {
            return await _notificationService.GetAll();
        }
    }
}
