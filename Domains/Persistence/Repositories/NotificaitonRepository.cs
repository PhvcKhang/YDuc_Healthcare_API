using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
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
            if (await ExistsAsync(notification.NotificaitonId))
            {
                throw new EntityDuplicationException(nameof(Notification), notification.NotificaitonId);
            }
            //Person doctor = _context.Persons.FirstOrDefault(x => x.PersonId == doctorId);

            return _context.Notifications.Add(notification).Entity;
        }
        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .OrderByDescending(x => x.SendAt)
                .ToListAsync();

        }
        public async Task<bool> ExistsAsync(string notificationId)
        {
            return await _context.Notifications
                .AnyAsync(x => x.NotificaitonId == notificationId);
        }
        public async Task<List<Notification>> GetByIdAsync(string doctorId)
        {
            return await _context.Notifications
                .Where(x => x.Doctor.PersonId == doctorId)
                .OrderByDescending(x => x.SendAt)
                .ToListAsync();
        }
        public Notification ChangeStatusAsync(string notificationId)
        {
            var notificaiton =  _context.Notifications
                                .Where(x => x.NotificaitonId == notificationId)
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
    }
}
