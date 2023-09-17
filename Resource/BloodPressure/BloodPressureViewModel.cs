namespace HealthCareApplication.Resource.BloodPressure;

public class BloodPressureViewModel
{
    public int Systolic { get; set; }
    public int PulseRate { get; set; }
    public string? ImageLink { get; set; } = null;
    public DateTime Timestamp { get; set; }

    public BloodPressureViewModel(int systolic, int pulseRate, string imageLink, DateTime timestamp)
    {
        Systolic = systolic;
        PulseRate = pulseRate;
        ImageLink = imageLink;
        Timestamp = timestamp;
    }
}
