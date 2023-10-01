//using HealthCareApplication.Migrations;

namespace HealthCareApplication.Domains.Models
{
    public class Notification
    {
        public string NotificationId { get; private set; }
        public string Heading { get; private set; } = "Thông báo";
        public string? Content { get; private set; }
        public string PatientId { get; private set; } 
        public string PatientName { get; private set; }
        public Person? Carer { get; private set; }
        public string? CarerId { get; private set; } 
        public BloodPressure? BloodPressure { get; private set; } = null;
        public string? BloodPressureId { get; private set; }
        public BloodSugar? BloodSugar { get; private set; } = null;
        public string? BloodSugarId { get; private set; }
        public BodyTemperature? BodyTemperature { get; private set; } = null;
        public string? BodyTemperatureId { get; private set; }
        public SpO2? SpO2 { get; private set; } = null;
        public string? SpO2Id { get; private set; }
        public ENotificationType Type { get; private set; } 
        public bool Seen { get; set; } = false;
        public DateTime SendAt { get; private set; }
        public Notification( string notificationId, string? content, DateTime sendAt,string patientId, string patientName, Person carer, BloodPressure? bloodPressure, BloodSugar? bloodSugar, BodyTemperature? bodyTemperature,SpO2? spO2, ENotificationType type)
        {
            NotificationId = notificationId;
            Content = content;
            PatientId = patientId;
            SendAt = sendAt;
            Carer = carer;
            PatientName = patientName;
            BloodPressure = bloodPressure;
            BloodSugar = bloodSugar;
            BodyTemperature = bodyTemperature;
            SpO2 = spO2;
            Type = type;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Notification() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
