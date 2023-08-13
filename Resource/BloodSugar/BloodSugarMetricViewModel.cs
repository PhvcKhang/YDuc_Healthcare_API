namespace HealthCareApplication.Resource.BloodSugar;

public class BloodSugarMetricViewModel
{
    public decimal Value { get; set; }

    public BloodSugarMetricViewModel(decimal value)
    {
        Value = value;
    }
}
