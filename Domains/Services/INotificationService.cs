using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Notification;

namespace HealthCareApplication.Domains.Services
{
    public interface INotificationService
    {
        Task<bool> CreateNotification(Notification notification);
        Task<List<NotificationViewModel>> GetAll();
        Task<List<NotificationViewModel>> GetByDoctorId(string doctorId, int startIndex, int lastIndex);
        Task<bool> ChangeStatus(string notificationId);
        Task<int> GetUnseenNotifications(string notificationId);
        Task<bool> Delete(string notificationId);
    }
}
