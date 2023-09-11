using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Notification;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HealthCareApplication.Resource.Persons.Patients
{
    [DataContract]
    public class AddNewRelativeViewModel
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

        public AddNewRelativeViewModel( string name, int age, string address, string phoneNumber, EPersonGender gender)
        {
            PersonId = "R" + DateTime.Now.ToString("fffffff") + phoneNumber.Substring(phoneNumber.Length - 10, 5);
            Name = name;
            Age = age;
            Address = address;
            PersonType = EPersonType.Relative;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }
    }
}
