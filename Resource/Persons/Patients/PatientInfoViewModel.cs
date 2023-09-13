using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Relatives;

namespace HealthCareApplication.Resource.Persons
{
    public class PatientInfoViewModel
    {
        public string PersonId { get; set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Address { get; private set; }
        public decimal Weight { get; private set; }
        public decimal Height { get; private set; }
        public List<RelativesViewModel> Relatives { get; set; } = new List<RelativesViewModel>();
        public DoctorsViewModel? Doctor { get; set; }
        public EPersonGender Gender { get; private set; } 
        public EPersonType PersonType { get; private set; }
        public string? PhoneNumber { get; private set; }
        public List<BloodPressureViewModel>? BloodPressures { get; private set; }
        public List<BloodSugarViewModel>? BloodSugars { get; private set; }
        public List<BodyTemperatureViewModel>? BodyTemperatures { get; private set; }

    }
}
