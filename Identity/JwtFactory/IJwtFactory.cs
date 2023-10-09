using System.Security.Claims;

namespace HealthCareApplication.Identity.Authentication
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, IEnumerable<string> roles);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
