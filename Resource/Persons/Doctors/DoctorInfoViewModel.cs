using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;

namespace HealthCareApplication.Resource.Persons.Doctors
{
    public class DoctorInfoViewModel
    {
        public string PersonId { get; set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public AddressViewModel? Address { get; private set; }
        public string? Avatar { get; private set; } = null;
        public EPersonType PersonType { get; private set; }
        public string? PhoneNumber { get; private set; }
        public List<PatientsViewModel>? Patients { get; private set; }
    }
}
