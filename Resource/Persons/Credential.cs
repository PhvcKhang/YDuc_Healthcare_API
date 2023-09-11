namespace HealthCareApplication.Resource.Persons
{
    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Credential(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
