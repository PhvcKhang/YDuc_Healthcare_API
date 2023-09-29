using HealthCareApplication.Domains.Models;

namespace HealthCareApplication.Identity.Resources
{
    public class RegistrationResource
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get;  set; }
        public int Age { get;  set; }
        public string Address { get; set; }
        public EPersonType PersonType { get; set; }
        public EPersonGender Gender { get;  set; }
        public decimal Weight { get;    set; }
        public decimal Height { get; set; }
        public string PhoneNumber { get; set; }
    }
}
