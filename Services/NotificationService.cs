using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Repositories;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Resource.Notification;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IPersonRepository _personRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IMapper mapper, IPersonRepository personRepository)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<bool> CreateNotification(Notification notification)
        {
            var result = await _notificationRepository.CreateAsync(notification);
            if(result is null)
            {
                return false;
            }
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<NotificationViewModel>> GetAll()
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var viewModel = _mapper.Map<List<Notification>, List<NotificationViewModel>>(notifications);
            return viewModel;
        }
        public async Task<bool> ChangeStatus(string notificationId)
        {
            _notificationRepository.ChangeStatusAsync(notificationId);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> GetUnseenNotifications(string personId)
        {
            var notificationsOfCarer = await _notificationRepository.GetByCarerIdAsync(personId);
            var unseenNotifications = notificationsOfCarer.Where(x => x.Seen == false).ToList();
            return unseenNotifications.Count;
        }

        public async Task<bool> Delete(string notificationId)
        {
            await _notificationRepository.DeleteAsync(notificationId);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> GetNumberOfNotifications(string personId)
        {

            var notificationsOfCarer = await _notificationRepository.GetByCarerIdAsync(personId);

            return notificationsOfCarer.Count;
        }

        public async Task<List<NotificationViewModel>> GetRangeById(string personId, int startIndex, int lastIndex)
        {
            var count = await GetNumberOfNotifications(personId);
            var countIndex = count - 1;
            int numberOfNotificationsToGet = lastIndex - startIndex + 1;

            if ( count == 0)
            {
                return new List<NotificationViewModel>() { };
            }

            if (numberOfNotificationsToGet >= count)
            {
                lastIndex = countIndex;
            }

            var notificationsOfCarer = await _notificationRepository.GetByCarerIdAsync(personId, startIndex, lastIndex);
            var viewModel = _mapper.Map<List<Notification>, List<NotificationViewModel>>(notificationsOfCarer);
            return viewModel;
        }
    }
}
