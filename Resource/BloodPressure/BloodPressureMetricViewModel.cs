namespace HealthCareApplication.Resource.BloodPressure;

public class BloodPressureMetricViewModel
{
    public int Systolic { get; set; }
    public int PulseRate { get; set; }

    public BloodPressureMetricViewModel(int systolic, int pulseRate)
    {
        Systolic = systolic;
        PulseRate = pulseRate;
    }
}
