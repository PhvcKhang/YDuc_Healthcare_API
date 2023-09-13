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
        [JsonIgnore]
        public int Age { get; private set; }
        [DataMember]
        [JsonIgnore]
        public string Address { get; private set; }
        [DataMember]
        [JsonIgnore]
        public EPersonType PersonType { get; private set; }
        [DataMember]
        public string PhoneNumber { get; private set; }
        [DataMember]
        [JsonIgnore]
        public EPersonGender Gender { get; private set; }

        public AddNewPatientViewModel(string name, string phoneNumber)
        {
            PersonId = "P" + DateTime.Now.ToString("fffffff") + phoneNumber.Substring(phoneNumber.Length - 10, 5);
            Name = name;
            Age = 0;
            Address = "Unkown";
            PersonType = EPersonType.Patient;
            PhoneNumber = phoneNumber;
            Gender = EPersonGender.Unkown;
        }
    }
}
