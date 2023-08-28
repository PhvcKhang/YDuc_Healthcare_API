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
        private readonly NotificaitonHelper _notificaitonHelper;

        public NotificationController()
        {
            _notificaitonHelper = new NotificaitonHelper();
        }
        [HttpPost]
        [Route("PushNotification")]
        public async Task<string> PushNotificationById(string doctorId, string patientId)
        {
           var content = "Bệnh nhân " +patientId+ " vừa cập nhật chỉ số";
           return await _notificaitonHelper.PushAsync(doctorId,content,patientId);
        }
        [HttpGet]
        [Route("{notificationId}")]
        public async Task<string> GetById([FromRoute] string notificationId)
        {
              return await _notificaitonHelper.GetByIdAsync(notificationId);
        }
        [HttpGet]
        [Route("All")]
        public async Task<string> GetAll()
        {
            return await _notificaitonHelper.GetAllAsync();
        }
    }
}
