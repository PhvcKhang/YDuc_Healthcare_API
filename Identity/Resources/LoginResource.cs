using HealthCareApplication.Domains.Models;
using HealthCareApplication.Identity.Models;

namespace HealthCareApplication.Identity.Resources
{
    public class LoginResource
    {
        public JwtToken Token { get; set; }
        public Person User { get; set; }
        public List<string> Roles { get; set; }
    }
}
