using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.BodyTemperature;

[DataContract]
public class CreateBodyTemperatureViewModel
{
    [DataMember]
    public decimal Value { get; set; }
    [DataMember]
    public string? ImageLink { get; set; } = null;

    public CreateBodyTemperatureViewModel(decimal value, string imageLink)
    {
        Value = value;
        ImageLink = imageLink;
    }
}
