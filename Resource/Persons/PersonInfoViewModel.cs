using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;

namespace HealthCareApplication.Resource.Persons
{
    public class PersonInfoViewModel
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public AddressViewModel? Address { get; private set; }
        public decimal Weight { get; private set; }
        public decimal Height { get; private set; }
        public string? Avatar { get; private set; } = null;
        public BloodPressureViewModel? LastBloodPressure { get; private set; } = null;
        public BloodSugarViewModel? LastBloodSugar { get; private set; } = null;
        public BodyTemperatureViewModel? LastBodyTemperature { get; private set; } = null;

    }
}
