using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Notification;

namespace HealthCareApplication.Domains.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<List<Notification>> GetAllAsync();
        Task<List<Notification>> GetByIdAsync(string doctorId, int startIndex, int lastIndex );
        Task<NumberOfNotifications > GetNumberOfNotificationsAsync(string doctorId);
        Notification ChangeStatusAsync(string notificationId);
        Task<int> GetUnseenNotificationsAsync(string doctorId);
        Task<Notification> DeleteAsync(string notificationId);
    }
}
