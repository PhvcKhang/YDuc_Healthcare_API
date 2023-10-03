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
    [Route("[controller]")]
    public class NotificationsController : Controller
    {
        private readonly NotificationHelper _notificaitonHelper;
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
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
        [Route("{personId}")]
        public async Task<List<NotificationViewModel>> GetRangeById([FromRoute] string personId, [FromQuery] int startIndex, [FromQuery] int lastIndex)
        {
            return await _notificationService.GetRangeById(personId, startIndex, lastIndex);
        }
        [HttpPatch]
        [Route("{notificationId}")]
        public async Task<IActionResult> ChangeStatus( [FromRoute] string notificationId)
        {
            try
            {
                var result = await _notificationService.ChangeStatus(notificationId);
                return new OkObjectResult($"seen: {result}");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         }
        [HttpGet]
        [Route("{personId}/Unseen")]
        public async Task<IActionResult> UnseenNotifications([FromRoute]string personId)
        {
            try
            {
                var result = await _notificationService.GetUnseenNotifications(personId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{personId}/Count")]
        public async Task<IActionResult> GetNumberOfNotifications([FromRoute]string personId)
        {
            try
            {
                var result = await _notificationService.GetNumberOfNotifications(personId);
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
