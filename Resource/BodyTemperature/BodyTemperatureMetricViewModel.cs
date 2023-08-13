namespace HealthCareApplication.Resource.BodyTemperature;

public class BodyTemperatureMetricViewModel
{
    public decimal Value { get; set; }

    public BodyTemperatureMetricViewModel(decimal value)
    {
        Value = value;
    }
}
