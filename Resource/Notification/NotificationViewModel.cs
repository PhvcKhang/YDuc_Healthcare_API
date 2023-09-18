using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using Newtonsoft.Json;
using System.Text.Json;

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
        public DateTime SendAt { get; set; }
        public BloodPressureViewModel? BloodPressure { get; set; }
        public BloodSugarViewModel? BloodSugar { get; set; }
        public BodyTemperatureViewModel? BodyTemperature { get; set; }
    }
}
