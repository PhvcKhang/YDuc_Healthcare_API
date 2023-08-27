using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace HealthCareApplication.OneSignal
{
    public class OneSignal
    {
        public async Task<bool> PushNotificationAsync()
        {

            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");
            request.AddJsonBody("{\"included_segments\":[\"Total Subscriptions\"],\"contents\":{\"en\":\"You have received a message from Backend team\",\"es\":\"Spanish Message\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\"}", false);
            var response = await client.PostAsync(request);

            return response.IsSuccessStatusCode;
        }

    }
}
