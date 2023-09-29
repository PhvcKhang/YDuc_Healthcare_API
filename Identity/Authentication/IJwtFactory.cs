using System.Security.Claims;

namespace HealthCareApplication.Identity.Authentication
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, string role);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
