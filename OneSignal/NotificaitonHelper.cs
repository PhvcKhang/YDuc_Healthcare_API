using HealthCareApplication.Migrations;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using HealthCareApplication.Domains.Models;
using OneSignal.RestAPIv3.Client.Resources;
using Azure.Core;
using Microsoft.Extensions.Options;

namespace HealthCareApplication.OneSignal
{
    public class NotificaitonHelper
    {
        public NotificaitonHelper()
        {
        }

        public async Task<string> PushAsync(string doctorId, string content, string patientId)
        {
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");

            request.AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctorId + "\"}],\"contents\":{\"en\":\""+content+"\",\"es\":\"Spanish Message\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\""+patientId+"\"}}", false);

            var response = await client.PostAsync(request);

            return response.Content.ToString().Substring(7, 36); ;
        }
        public async Task<string> GetByIdAsync(string notificationId)
        {
            //2ad3ec97 - 9417 - 42a7 - aa6f - 3b58977c4ae5
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications/"+notificationId+"?app_id=eb1e614e-54fe-4824-9c1a-aad236ec92d3");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");
            var response = await client.GetAsync(request);

            var result= response.Content;

            return result;
        }
        public async Task<string> GetAllAsync()
        {
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications?app_id=eb1e614e-54fe-4824-9c1a-aad236ec92d3&limit=5&kind=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");
            var response = await client.GetAsync(request);
            return response.Content;
        }

    }
}
