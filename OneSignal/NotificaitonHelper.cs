using HealthCareApplication.Migrations;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using HealthCareApplication.Domains.Models;
using OneSignal.RestAPIv3.Client.Resources;

namespace HealthCareApplication.OneSignal
{
    public class NotificaitonHelper
    {

        public async Task<bool> PushAsync(string doctorId)
        {
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");

            request.AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctorId + "\"}],\"contents\":{\"en\":\"You have received a message\",\"es\":\"Spanish Message\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"123\"}}", false);

            var response = await client.PostAsync(request);

            //Create Notification
            string id = response.Content.ToString().Substring(7, 36);
            //var notification = new Notification(id, content, DateTime.Now, patientId);

            return response.IsSuccessStatusCode;
        }
        public async Task<string> GetAsync()
        {
            //2ad3ec97 - 9417 - 42a7 - aa6f - 3b58977c4ae5
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications/2ad3ec97-9417-42a7-aa6f-3b58977c4ae5?app_id=eb1e614e-54fe-4824-9c1a-aad236ec92d3");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");
            var response = await client.GetAsync(request);

            var result= response.Content;

            return result;

        }
    }
}
