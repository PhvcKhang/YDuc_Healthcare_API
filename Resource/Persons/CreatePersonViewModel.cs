using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.Persons;

[DataContract]
public class CreatePersonViewModel
{
    [DataMember]
    public string PersonId { get; private set; }
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
    public EPersonGender Gender { get; private set; }


    public CreatePersonViewModel( string name, int age, EPersonType personType, CreateAddressViewModel address, decimal weight, decimal height, string phoneNumber, EPersonGender gender)
    {
        Name = name;
        Age = age;
        PersonType = personType;
        Address = address;
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        Gender = gender;

        //Create Person ID depending on type
        switch (personType)
        {
            case EPersonType.Doctor:
                PersonId = "D" + DateTime.Now.ToString("fffffff") + phoneNumber.Substring(phoneNumber.Length - 10 , 5);
                break;
            case EPersonType.Patient:
                PersonId = "P" + DateTime.Now.ToString("fffffff") + phoneNumber.Substring(phoneNumber.Length - 10 , 5);
                break;
            case EPersonType.Relative:
                PersonId = "R" + DateTime.Now.ToString("fffffff") + phoneNumber.Substring(phoneNumber.Length - 10, 5);
                break;
        }

    }
}
