using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Notification;

namespace HealthCareApplication.Domains.Services
{
    public interface INotificationService
    {
        Task<List<NotificationViewModel>> GetByPersonId(string personId);
        Task<bool> CreateNotification(Notification notification);
    }
}
