using HealthCareApplication.Migrations;

namespace HealthCareApplication.Domains.Models
{
    public class Notification
    {
        public string NotificaitonId { get; set; }
        public string Heading { get; set; } = "Thông báo";
        public string? Content { get; set; }
        public string PatientId { get; set; } //send
        public string PatientName { get; set; }
        public Person? Doctor { get; set; } //receiver
        public bool Seen { get; set; } = false;
        public DateTime SendAt { get; set; }
        public Notification(string notificaitonId, string? content, string patientId, DateTime sendAt, Person doctor, string patientName)
        {
            NotificaitonId = notificaitonId;
            Content = content;
            PatientId = patientId;
            SendAt = sendAt;
            Doctor = doctor;
            PatientName = patientName;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Notification() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
