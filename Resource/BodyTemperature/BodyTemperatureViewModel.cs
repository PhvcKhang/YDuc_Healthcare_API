namespace HealthCareApplication.Resource.BodyTemperature;

public class BodyTemperatureViewModel
{
    public decimal Value { get; set; }
    public string ImageLink { get; set; }
    public DateTime Timestamp { get; set; }

    public BodyTemperatureViewModel(decimal value, string imageLink, DateTime timestamp)
    {
        Value = value;
        ImageLink = imageLink;
        Timestamp = timestamp;
    }
}
