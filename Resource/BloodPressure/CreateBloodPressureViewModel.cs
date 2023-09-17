using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.BloodPressure;

[DataContract]
public class CreateBloodPressureViewModel
{
    [DataMember]
    public int Systolic { get;set; }

    [DataMember]
    public int PulseRate { get; set; }
    [DataMember]
    public string? ImageLink { get; set; } = null;

    public CreateBloodPressureViewModel(int systolic, int pulseRate, string imageLink)
    {
        Systolic = systolic;
        PulseRate = pulseRate;
        ImageLink = imageLink;
    }
}
