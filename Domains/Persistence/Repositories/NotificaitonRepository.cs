using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
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
            return  _context.Notifications.Add(notification).Entity;
        }
        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .ToListAsync();
        }
        public async Task<bool> ExistsAsync(string notificationId)
        {
            return await _context.Notifications
                .AnyAsync(x => x.NotificaitonId == notificationId);
        }
    }
}
