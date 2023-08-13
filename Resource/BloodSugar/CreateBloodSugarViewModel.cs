using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.BloodSugar;

[DataContract]
public class CreateBloodSugarViewModel
{
    [DataMember]
    public decimal Value { get; set; }
    [DataMember]
    public string ImageLink { get; set; }

    public CreateBloodSugarViewModel(decimal value, string imageLink)
    {
        Value = value;
        ImageLink = imageLink;
    }
}
