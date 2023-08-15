using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.Persons;

[DataContract]
public class CreatePersonViewModel
{
    [DataMember]
    public string PersonId { get; set; }
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public int Age { get; set; }
    [DataMember]
    public EPersonType PersonType { get; set; }
    [DataMember]
    public CreateAddressViewModel Address { get; set; }
    [DataMember]
    public decimal Weight { get; set; }
    [DataMember]
    public decimal Height { get; set; }
    [DataMember]
    public string PhoneNumber { get; private set; }
    [DataMember]
    public string Avatar { get; private set; }


    public CreatePersonViewModel(string personId, string name, int age, EPersonType personType, CreateAddressViewModel address, decimal weight, decimal height, string phoneNumber, string avatar)
    {
        PersonId = personId;
        Name = name;
        Age = age;
        PersonType = personType;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        Avatar = avatar;

    }
}
