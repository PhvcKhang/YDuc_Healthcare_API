using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;

namespace HealthCareApplication.Domains.Models;

public class Person
{
    public string PersonId { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
    public EPersonType PersonType { get; private set; }
    public Address Address { get; private set; }
    public int AddressId { get; private set; }
    public decimal Weight { get; private set; }
    public decimal Height { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? Avatar { get; private set; } = null;
    public List<Person> Patients { get; private set; }
    public List<BloodPressure> BloodPressures { get; private set; }
    public List<BloodSugar> BloodSugars { get; private set; }
    public List<BodyTemperature> BodyTemperatures { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Person() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Person(string personId, string name, int age, EPersonType personType, Address address, decimal weight, decimal height, string phoneNumber, string avatar, List<Person> patients, List<BloodPressure> bloodPressures, List<BloodSugar> bloodSugars, List<BodyTemperature> bodyTemperatures)
    {
        PersonId = personId;
        Name = name;
        Age = age;
        PersonType = personType;
        Address = address;
        Weight = weight;
        Height = height;
        Patients = patients;
        BloodPressures = bloodPressures;
        BloodSugars = bloodSugars;
        BodyTemperatures = bodyTemperatures;
        PhoneNumber = phoneNumber;
        Avatar = avatar;
    }

    public void Update(string name, int age, EPersonType personType, Address address, decimal weight, decimal height, string phoneNumber, string avatar)
    {
        Name = name;
        Age = age;
        PersonType = personType;
        Address.Update(address);
        Weight = weight;
        Height = height;
        PhoneNumber = phoneNumber;
        Avatar = avatar;
    }
}
