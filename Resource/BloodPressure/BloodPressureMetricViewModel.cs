namespace HealthCareApplication.Resource.BloodPressure;

public class BloodPressureMetricViewModel
{
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int PulseRate { get; set; }

    public BloodPressureMetricViewModel(int systolic, int diastolic, int pulseRate)
    {
        Systolic = systolic;
        Diastolic = diastolic;
        PulseRate = pulseRate;
    }
}
