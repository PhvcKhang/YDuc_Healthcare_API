using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons.Doctors;

namespace HealthCareApplication.Resource.Users.Admin
{
    public class AdminViewModel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public EPersonType PersonType { get; private set; }
        public string? PhoneNumber { get; private set; }
        public List<DoctorsViewModel>? Doctors { get; set; } = new List<DoctorsViewModel>();
    }
}
