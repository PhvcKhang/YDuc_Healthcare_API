using HealthCareApplication.Migrations;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using HealthCareApplication.Domains.Models;
using OneSignal.RestAPIv3.Client.Resources;
using Azure.Core;
using Microsoft.Extensions.Options;
using OneSignal.RestAPIv3.Client.Resources.Notifications;
using HealthCareApplication.Domains.Services;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.OneSignal
{
    public class NotificationHelper
    {
        public IPersonService _personService { get; set; }

        public NotificationHelper( IPersonService personService)
        {
            _personService = personService;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public NotificationHelper()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public async Task<Notification> PushAsync( string patientId, Person doctor, string patientName, string VIcontent, string ENcontent, BloodPressure? bloodPressure = null, BloodSugar? bloodSugar = null, BodyTemperature? bodyTemperature = null)
        {

            //var doctorId = "240914";

            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications");
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");

            if (ENcontent.Contains("blood pressure"))
            {
                request.AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patientId + "\",\"Systolic\":\"" + bloodPressure?.Systolic + "\",\"PlusRate\":\"" + bloodPressure?.PulseRate + "\", \"ImageLink\":\"" + bloodPressure?.ImageLink + "\", \"UpdatedDate\":\"" + bloodPressure?.Timestamp + "\",\"Indicator\":\"BloodPressure\"},\"large_icon\":\"" + bloodPressure?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
            }
            else if (ENcontent.Contains("blood sugar"))
            {
                request.AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patientId + "\",\"BloodSugar\":\"" + bloodSugar?.Value + "\",\"ImageLink\":\"" + bloodSugar?.ImageLink + "\", \"UpdatedDate\":\"" + bloodSugar?.Timestamp + "\",\"Indicator\":\"BloodSugar\"},\"large_icon\":\"" + bloodSugar?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
            }
            else if (ENcontent.Contains("body temperature"))
            {
                request.AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patientId + "\",\"BodyTemperature\":\"" + bodyTemperature?.Value + "\",\"ImageLink\":\"" + bodyTemperature?.ImageLink + "\",\"UpdatedDate\":\"" + bodyTemperature?.Timestamp + "\",\"Indicator\":\"BodyTemperature\"},\"large_icon\":\"" + bodyTemperature?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
            }
            var response = await client.PostAsync(request);

            var notificationId = response.Content?.ToString().Substring(7, 36); 

            //Return Notification object 
            var notification = new Notification(notificationId, VIcontent, patientId ,DateTime.Now, doctor, patientName, bloodPressure, bloodSugar, bodyTemperature);
            return notification;

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
