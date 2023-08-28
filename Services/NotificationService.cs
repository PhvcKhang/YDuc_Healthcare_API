using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Repositories;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;

namespace HealthCareApplication.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateNotification(Notification notification)
        {
            await _notificationRepository.CreateAsync(notification);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Notification>> GetAll()
        {
            return await _notificationRepository.GetAllAsync();
        }
    }
}
