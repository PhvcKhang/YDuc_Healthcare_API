using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Domains.Services
{
    public interface INotificationService
    {
        Task<bool> CreateNotification(Notification notification);
        Task<List<Notification>> GetAll();
    }
}
