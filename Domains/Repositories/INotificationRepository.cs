using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Notification;

namespace HealthCareApplication.Domains.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<List<Notification>> GetAllAsync();
        Task<List<Notification>> GetByCarerIdAsync(string carerId);
        Task<int> GetNumberOfNotificationsAsync(string carerId);
        Notification ChangeStatusAsync(string notificationId);
        Task<Notification> DeleteAsync(string notificationId);
    }
}
