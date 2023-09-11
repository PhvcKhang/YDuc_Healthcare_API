using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;

namespace HealthCareApplication.Resource.Persons
{
    public class PatientsViewModel
    {
        public string PersonId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

    }
}
