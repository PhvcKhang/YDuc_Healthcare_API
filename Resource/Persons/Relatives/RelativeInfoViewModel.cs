using HealthCareApplication.Domains.Models;


namespace HealthCareApplication.Resource.Persons.Relatives
{
    public class RelativeInfoViewModel
    {
        public string PersonId { get; set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Address { get; private set; }
        public EPersonGender Gender { get; private set; }
        public EPersonType PersonType { get; private set; }
        public string? PhoneNumber { get; private set; }
        public List<PatientsViewModel>? Patients { get; private set; }
    }
}
