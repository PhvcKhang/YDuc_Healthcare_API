using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Resource.Notification;

namespace HealthCareApplication.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateNotification(Notification notification)
        {
             await _notificationRepository.CreateAsync(notification);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<NotificationViewModel>> GetByPersonId(string personId)
        {
            var notifications = await _notificationRepository.GetNotificationsAsync(personId);
            return _mapper.Map<List<Notification>,List<NotificationViewModel>>(notifications);
        }
    }
}
