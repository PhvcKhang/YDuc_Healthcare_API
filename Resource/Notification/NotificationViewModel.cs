using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Resource.Notification
{
    public class NotificationViewModel
    {
        public string NotificaitonId { get; private set; }
        public string? Heading { get; private set; }
        public string? Content { get; private set; }
        public DateTime SendDate { get; private set; }
        public bool Seen { get; private set; } = false;
        public string? DoctorId { get; private set; }
        public string? PersonId { get; private set; }
    }
}
