using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Extensions.Exceptions;
//using HealthCareApplication.Migrations;
using HealthCareApplication.Resource.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;

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
                
            return _context.Notifications.Add(notification).Entity;
        }
        public async Task<List<Notification>> GetAllAsync()
        {
            var notifications = await _context.Notifications
                .OrderByDescending(x => x.SendAt)
                .Include(x => x.BloodPressure)
                .Include(x => x.BloodSugar)
                .Include(x => x.BodyTemperature)
                .Include(x => x.SpO2)
                .ToListAsync();
            return notifications;

        }
        public async Task<bool> ExistsAsync(string notificationId)
        {
            return await _context.Notifications
                .AnyAsync(x => x.NotificationId == notificationId);
        }
        public async Task<Notification> ChangeStatusAsync(string notificationId)
        {
            var notificaiton =  await _context.Notifications
                                .FirstOrDefaultAsync(x => x.NotificationId == notificationId);
            notificaiton.Seen = true;
            return _context.Notifications.Update(notificaiton).Entity;

        }
        public async Task<Notification> DeleteAsync(string notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(x => x.NotificationId == notificationId) ?? throw new ResourceNotFoundException(nameof(Notification), notificationId);
            return _context.Notifications
                .Remove(notification)
                .Entity;
        }

        public async Task<int> GetNumberOfNotificationsAsync(string doctorId)
        {
            var doctor = await _context.Persons.FirstOrDefaultAsync(x => x.Id == doctorId) ?? throw new ResourceNotFoundException(nameof(Person), doctorId);
            List<Notification> notifications = await _context.Notifications.Where(x => x.CarerId == doctorId).ToListAsync();

            return notifications.Count;
        }

        public async Task<List<Notification>> GetByCarerIdAsync(string carerId, int startIndex, int lastIndex)
        {
            var source = await _context.Notifications
            .Where(x => x.CarerId == carerId)
            .ToListAsync();

            //Explicit loading
            var notifications = source.OrderByDescending(x => x.SendAt).ToList().GetRange(startIndex, lastIndex - startIndex + 1);
            foreach(var notification in notifications)
            {
                await _context.Entry(notification).Reference(x => x.BloodPressure).LoadAsync();
                await _context.Entry(notification).Reference(x => x.BloodSugar).LoadAsync();
                await _context.Entry(notification).Reference(x => x.BodyTemperature).LoadAsync();
                await _context.Entry(notification).Reference(x => x.SpO2).LoadAsync();
            }
            return notifications;
        }

        public async Task<List<Notification>> GetByCarerIdAsync(string carerId)
        {
            var notifications = await _context.Notifications.Where(x => x.CarerId == carerId).ToListAsync();
            return notifications;
        }
    }
}
