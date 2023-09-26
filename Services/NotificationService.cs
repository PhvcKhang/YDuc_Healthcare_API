using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Repositories;
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

        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateNotification(Notification notification)
        {
            await _notificationRepository.CreateAsync(notification);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<NotificationViewModel>> GetAll()
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var viewModel = _mapper.Map<List<Notification>, List<NotificationViewModel>>(notifications);
            return viewModel;
        }
        public async Task<List<NotificationViewModel>> GetByDoctorId(string doctorId, int startIndex, int lastIndex)
        {
            var notifications = await _notificationRepository.GetByIdAsync(doctorId, startIndex, lastIndex);
            var viewModel = _mapper.Map<List<Notification>, List<NotificationViewModel>>(notifications);
            return viewModel;
        }
        public async Task<bool> ChangeStatus(string notificationId)
        {
             _notificationRepository.ChangeStatusAsync(notificationId);
            return await _unitOfWork.CompleteAsync();
        }

        public Task<int> GetUnseenNotifications(string doctorId)
        {
            return _notificationRepository.GetUnseenNotificationsAsync(doctorId);
        }

        public async Task<bool> Delete(string notificationId)
        {
            await _notificationRepository.DeleteAsync(notificationId);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<NumberOfNotifications> GetNumberOfNotifications(string doctorId)
        {
            return await _notificationRepository.GetNumberOfNotificationsAsync(doctorId);
        }
    }
}
