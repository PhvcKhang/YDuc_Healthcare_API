using HealthCareApplication.Identity.Authentication;
using HealthCareApplication.Identity.Models;
using System.Security.Claims;

namespace HealthCareApplication.Identity.Helpers
{
    public class Token
    {
        public static async Task<JwtToken> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory,string userName, string role, JwtIssuerOptions jwtOptions)
        {
            var token = new JwtToken
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(userName, identity, role),
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
            };
            return token;
        }
    }
}
