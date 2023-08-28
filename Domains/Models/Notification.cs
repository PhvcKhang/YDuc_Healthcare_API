namespace HealthCareApplication.Domains.Models
{
    public class Notification
    {
        public string NotificaitonId { get;  set; }
        public string? Heading { get; set; } = "Notification";
        public string? Content { get; set; }
        public DateTime SendDate { get; set; }
        public bool Seen { get; set; } = false;
        public string? PatientId { get; set; }
        public Notification(string notificationId, string? content, DateTime sendDate, string patientId)
        {
            NotificaitonId = notificationId;
            Content = content;
            SendDate = sendDate;
            PatientId = patientId;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Notification() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
