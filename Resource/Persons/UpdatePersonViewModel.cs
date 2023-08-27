using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.Persons;

[DataContract]
public class UpdatePersonViewModel
{
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public int Age { get; set; }
    [DataMember]
    public EPersonType PersonType { get; set; }
    [DataMember]
    public UpdateAddressViewModel Address { get; set; }
    [DataMember]
    public decimal Weight { get; set; }
    [DataMember]
    public decimal Height { get; set; }
    [DataMember]
    public string PhoneNumber { get; private set; }
    [DataMember]
    public EPersonGender Gender { get; private set; }

    public UpdatePersonViewModel(string name, int age, EPersonType personType, UpdateAddressViewModel address, decimal weight, decimal height, string phoneNumber, EPersonGender gender)
    {
        Name = name;
        Age = age;
        PersonType = personType;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        Gender = gender;
    }
}
