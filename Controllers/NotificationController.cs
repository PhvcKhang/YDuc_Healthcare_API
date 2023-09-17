using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.OneSignal;
using HealthCareApplication.Resource.Notification;
using MesMicroservice.Api.Application.Messages;
using Microsoft.AspNetCore.Authorization;
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
        [Route("{doctorId}/startIndex={startIndex}/lastIndex={lastIndex}")]
        public async Task<List<NotificationViewModel>> GetByDoctorId([FromRoute] string doctorId, [FromRoute] int startIndex, [FromRoute] int lastIndex)
        {
            return await _notificationService.GetByDoctorId(doctorId, startIndex, lastIndex);
        }
        [HttpPut]
        [Route("{notificationId}")]
        public async Task<IActionResult> ChangeStatus( [FromRoute] string notificationId)
        {
            try
            {
                var result = await _notificationService.ChangeStatus(notificationId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         }
        [HttpGet]
        [Route("{doctorId}/Unseen")]
        public async Task<int> UnseenNotifications([FromRoute]string doctorId)
        {
            return await _notificationService.GetUnseenNotifications(doctorId);
        }
        [HttpDelete]
        [Route("{notificationId}")]
        public async Task<IActionResult> Delete(string notificationId)
        {
            try
            {
                var result = await _notificationService.Delete(notificationId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        } 
    }
}
