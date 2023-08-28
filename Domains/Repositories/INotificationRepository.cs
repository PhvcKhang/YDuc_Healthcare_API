using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotificationsAsync(string personId);
        Task<Notification> CreateAsync(Notification notification);
    }
}
