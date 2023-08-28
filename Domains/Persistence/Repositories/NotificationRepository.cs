using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Domains.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            return _context.Notifications.Add(notification).Entity;
        }

        public async Task<List<Notification>> GetNotificationsAsync(string personId)
        {
            return await _context.Notifications.Where(x => x.PatientId == personId).ToListAsync();
        }
    }
}
