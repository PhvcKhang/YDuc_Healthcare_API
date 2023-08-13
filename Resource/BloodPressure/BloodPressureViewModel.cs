namespace HealthCareApplication.Resource.BloodPressure;

public class BloodPressureViewModel
{
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int PulseRate { get; set; }
    public string ImageLink { get; set; }
    public DateTime Timestamp { get; set; }

    public BloodPressureViewModel(int systolic, int diastolic, int pulseRate, string imageLink, DateTime timestamp)
    {
        Systolic = systolic;
        Diastolic = diastolic;
        PulseRate = pulseRate;
        ImageLink = imageLink;
        Timestamp = timestamp;
    }
}
