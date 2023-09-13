namespace HealthCareApplication.Domains.Models.ManyMany
{
    public class PersonRelative
    {
        public string PersonId { get; set; }
        public Person Person { get; set;}
        public string RelativeId { get; set; }
        public Person Relative { get; set; }
    }
}
