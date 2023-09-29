using Microsoft.IdentityModel.Tokens;

namespace HealthCareApplication.Identity.Models
{
    public class JwtIssuerOptions
    {
        //"iss" (Issuer) Claim - The "iss" (issuer) claim identifies the principal that issued the JWT.
        public string Issuer { get; set; }

        //"sub" (Subject) Claim - The "sub" (subject) claim identifies the principal that is the subject of the JWT.
        public string Subject { get; set; }

        //"aud" (Audience) Claim - The "aud" (audience) claim identifies the recipients that the JWT is intended for.
        public string Audience { get; set; }

        //"exp" (Expiration Time) Claim - The "exp" (expiration time) claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        //"nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
        public DateTime NotBefore => DateTime.UtcNow;

        //"iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time at which the JWT was issued.
        public DateTime IssuedAt => DateTime.UtcNow;

        //Set the timespan the token will be valid for
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(60);

        //"jti" (JWT ID) Claim (default ID is a GUID) 
        public Func<Task<string>> JtiGenerator => 
            () => Task.FromResult(Guid.NewGuid().ToString());

        // The signing key to use when generating tokens.
        public SigningCredentials SigningCredentials { get; set; }

    }
}
