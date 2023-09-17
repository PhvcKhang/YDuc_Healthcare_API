using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Persons.Doctors;
using Newtonsoft.Json;

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
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public BloodPressureViewModel? BloodPressure { get; set; } = null;
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public BloodSugarViewModel? BloodSugar { get; set; } = null;
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public BodyTemperatureViewModel? BodyTemperature { get; set; } = null; 
    }
}
