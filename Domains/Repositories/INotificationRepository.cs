using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<List<Notification>> GetAllAsync();
        Task<List<Notification>> GetByIdAsync(string doctorId);
        Notification ChangeStatusAsync(string notificationId);
    }
}
