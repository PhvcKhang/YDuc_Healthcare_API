using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Extensions.Exceptions;
using HealthCareApplication.Migrations;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCareApplication.Domains.Persistence.Repositories
{
    public class NotificaitonRepository : BaseRepository, INotificationRepository

    {
        public NotificaitonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            if (await ExistsAsync(notification.NotificationId))
            {
                throw new EntityDuplicationException(nameof(Notification), notification.NotificationId);
            }
            //Person doctor = _context.Persons.FirstOrDefault(x => x.PersonId == doctorId);

            return _context.Notifications.Add(notification).Entity;
        }
        public async Task<List<Notification>> GetAllAsync()
        {
            var notifications = await _context.Notifications
                .OrderByDescending(x => x.SendAt)
                .Include(x => x.BloodPressure)
                .Include(x => x.BloodSugar)
                .Include(x => x.BodyTemperature)
                .ToListAsync();
            return notifications;

        }
        public async Task<bool> ExistsAsync(string notificationId)
        {
            return await _context.Notifications
                .AnyAsync(x => x.NotificationId == notificationId);
        }
        public async Task<List<Notification>> GetByIdAsync(string doctorId, int startIndex, int lastIndex)
        {

            var notifications = await _context.Notifications
                .Include(x => x.BloodPressure)
                .Include(x => x.BloodSugar)
                .Include(x => x.BodyTemperature)
                .Where(x => x.Doctor.PersonId == doctorId)
                .OrderByDescending(x => x.SendAt)
                .ToListAsync();
            var result = notifications.GetRange(startIndex, lastIndex);
            return result;
        }
        public Notification ChangeStatusAsync(string notificationId)
        {
            var notificaiton =  _context.Notifications
                                .Where(x => x.NotificationId == notificationId)
                                .FirstOrDefault();
            notificaiton.Seen = true;
            return _context.Notifications.Update(notificaiton).Entity;

        }

        public async Task<int> GetUnseenNotificationsAsync(string doctorId)
        {
            var notifications = await _context.Notifications.Where(x => x.Doctor.PersonId == doctorId).ToListAsync();
            notifications.RemoveAll(x => x.Seen ==  true);
            var unseenCounter = notifications.Count();
            return unseenCounter;
        }
        public async Task<Notification> DeleteAsync(string notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(x => x.NotificationId == notificationId) ?? throw new ResourceNotFoundException(nameof(Notification), notificationId);
            return _context.Notifications
                .Remove(notification)
                .Entity;
        }
    }
}
