using HealthCareApplication.Migrations;

namespace HealthCareApplication.Domains.Models
{
    public class Notification
    {
        public string NotificationId { get; private set; }
        public string Heading { get; private set; } = "Thông báo";
        public string? Content { get; private set; }
        public string PatientId { get; private set; } 
        public string PatientName { get; private set; }
        public Person? Doctor { get; private set; } 
        public BloodPressure? BloodPressure { get; private set; } = null;
        public string? BloodPressureId { get; private set; }
        public BloodSugar? BloodSugar { get; private set; } = null;
        public string? BloodSugarId { get; private set; }
        public BodyTemperature? BodyTemperature { get; private set; } = null;
        public string? BodyTemperatureId { get; private set; }
        public bool Seen { get; set; } = false;
        public DateTime SendAt { get; private set; }
        public Notification(string notificationId, string? content, string patientId, DateTime sendAt, Person doctor, string patientName, BloodPressure? bloodPressure, BloodSugar? bloodSugar, BodyTemperature? bodyTemperature)
        {
            NotificationId = notificationId;
            Content = content;
            PatientId = patientId;
            SendAt = sendAt;
            Doctor = doctor;
            PatientName = patientName;
            BloodPressure = bloodPressure;
            BloodSugar = bloodSugar;
            BodyTemperature = bodyTemperature;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Notification() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
