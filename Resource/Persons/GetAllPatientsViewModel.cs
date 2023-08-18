using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;

namespace HealthCareApplication.Resource.Persons
{
    public class GetAllPatientsViewModel
    {
        public string PersonId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public EPersonType PersonType { get; set; }
        public AddressViewModel? Address { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string PhoneNumber { get; private set; }
        public string? Avatar { get; private set; } = null;
        public List<BloodPressureViewModel> BloodPressures { get; private set; }
        public List<BloodSugarViewModel> BloodSugars { get; private set; } 
        public List<BodyTemperatureViewModel> BodyTemperatures { get; private set; }
    
    }
}
