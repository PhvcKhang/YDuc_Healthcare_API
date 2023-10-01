using HealthCareApplication.Identity.Authentication;
using HealthCareApplication.Identity.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace HealthCareApplication.Identity.Helpers
{
    public class Tokens
    {
        public static async Task<JwtToken> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, IEnumerable<string> roles, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var token = new JwtToken
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(userName, identity, roles),
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds
            };
            return token;
        }
    }
}
