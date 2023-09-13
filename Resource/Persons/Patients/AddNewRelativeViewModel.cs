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

        public AddNewRelativeViewModel( string name, string phoneNumber)
        {
            PersonId = "R" + DateTime.Now.ToString("fffffff") + phoneNumber.Substring(phoneNumber.Length - 10, 5);
            Name = name;
            Age = 0;
            Address = "Unkown";
            PersonType = EPersonType.Relative;
            PhoneNumber = phoneNumber;
            Gender = EPersonGender.Unkown;
        }
    }
}
