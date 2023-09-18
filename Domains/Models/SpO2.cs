namespace HealthCareApplication.Domains.Models
{
    public class SpO2
    {
        public string SpO2Id { get; private set; }
        public int Value { get; private set; }
        public string? ImageLink { get; private set; } = null;
        public DateTime Timestamp { get; private set; }
        public Person Person { get; private set; }
        public Notification? Notification { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private SpO2() { }

        public SpO2(int value, string imageLink, DateTime timestamp, Person person)
        {
            Value = value;
            ImageLink = imageLink;
            Timestamp = timestamp;
            Person = person;
        }
    }
}
