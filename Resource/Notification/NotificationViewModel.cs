using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.SpO2;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HealthCareApplication.Resource.Notification
{
    public class NotificationViewModel
    {
        public string NotificationId { get; set; }
        public string Heading { get; set; } 
        public string? Content { get; set; }
        public string PatientId { get; set; } 
        public string PatientName { get; set; }
        public bool Seen { get; set; } = false; 
        public ENotificationType Type { get; set; }
        public DateTime SendAt { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BloodPressureViewModel? BloodPressure { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BloodSugarViewModel? BloodSugar { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BodyTemperatureViewModel? BodyTemperature { get; set; }
        public SpO2ViewModel? SpO2 { get; set; }

    }
}
