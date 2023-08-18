using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.BloodPressure;

[DataContract]
public class CreateBloodPressureViewModel
{
    [DataMember]
    public int Systolic { get;set; }
    [DataMember]
    public int Diastolic { get; set; }
    [DataMember]
    public int PulseRate { get; set; }
    [DataMember]
    public string? ImageLink { get; set; } = null;

    public CreateBloodPressureViewModel(int systolic, int diastolic, int pulseRate, string imageLink)
    {
        Systolic = systolic;
        Diastolic = diastolic;
        PulseRate = pulseRate;
        ImageLink = imageLink;
    }
}
