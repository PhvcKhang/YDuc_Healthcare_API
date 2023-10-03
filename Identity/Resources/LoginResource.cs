using HealthCareApplication.Domains.Models;
using HealthCareApplication.Identity.Models;
using HealthCareApplication.Resource.Users;

namespace HealthCareApplication.Identity.Resources
{
    public class LoginResource
    {
        public JwtToken Token { get; set; }
        public UserViewModel User { get; set; }
        public List<string> Roles { get; set; }
    }
}
