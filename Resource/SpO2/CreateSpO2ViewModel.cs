using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.SpO2;

[DataContract]
public class CreateSpO2ViewModel
{
    [DataMember]
    public int Value { get; set; }
    [DataMember]
    public string? ImageLink { get; set; } = null;

    public CreateSpO2ViewModel(int value, string imageLink)
    {
        Value = value;
        ImageLink = imageLink;
    }
}
