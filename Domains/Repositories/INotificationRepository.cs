using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<List<Notification>> GetAllAsync();
        Task<List<Notification>> GetByIdAsync(string doctorId, int startIndex, int lastIndex );
        Notification ChangeStatusAsync(string notificationId);
        Task<int> GetUnseenNotificationsAsync(string doctorId);
        Task<Notification> DeleteAsync(string notificationId);
    }
}
