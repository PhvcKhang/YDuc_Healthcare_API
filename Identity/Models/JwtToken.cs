namespace HealthCareApplication.Identity.Models
{
    public class JwtToken
    {
        public string Id { get; set; }
        public string AuthToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
