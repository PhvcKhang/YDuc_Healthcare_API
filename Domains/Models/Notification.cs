namespace HealthCareApplication.Domains.Models
{
    public class Notification
    {
        public string NotificaitonId { get; set; }
        public string Heading { get; set; } = "Notification";
        public string? Content { get; set; }
        public string? PatietnId { get; set; } //send
        public Person? Doctor { get; set; } //receiver
        public bool seen { get; set; } = false;
        public DateTime SendAt { get; set; }

        public Notification(string notificaitonId, string? content, string? patietnId, DateTime sendAt)
        {
            NotificaitonId = notificaitonId;
            Content = content;
            PatietnId = patietnId;
            SendAt = sendAt;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Notification() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
