using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons.Doctors;

namespace HealthCareApplication.Resource.Notification
{
    public class NotificationViewModel
    {
        public string NotificaitonId { get; set; }
        public string Heading { get; set; } 
        public string? Content { get; set; }
        public string PatientId { get; set; } 
        public string PatientName { get; set; }
        public bool Seen { get; set; } = false;
        public DateTime SendAt { get; set; }
    }
}
