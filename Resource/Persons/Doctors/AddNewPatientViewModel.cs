using HealthCareApplication.Domains.Models;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HealthCareApplication.Resource.Persons.Doctors
{
    [DataContract]
    public class AddNewPatientViewModel
    {
        [DataMember]
        public string PersonId { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public int Age { get; private set; }
        [DataMember]
        public string Address { get; private set; }
        [JsonIgnore]
        public EPersonType PersonType { get; private set; }
        [DataMember]
        public string PhoneNumber { get; private set; }
        [DataMember]
        public EPersonGender Gender { get; private set; }

        public AddNewPatientViewModel(string name, int age, string address, string phoneNumber, EPersonGender gender)
        {
            PersonId = "P" + DateTime.Now.ToString("fffffff") + phoneNumber.Substring(phoneNumber.Length - 10, 5);
            Name = name;
            Age = age;
            Address = address;
            PersonType = EPersonType.Patient;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }
    }
}
