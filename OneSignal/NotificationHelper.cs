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
using HealthCareApplication.Resource.Persons.Relatives;

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

        public async Task<Notification> PushAsync( Person patient, Person doctor, List<Person> relatives, string VIcontent, string ENcontent, ENotificationType notificationType, BloodPressure? bloodPressure = null, BloodSugar? bloodSugar = null, BodyTemperature? bodyTemperature = null, SpO2? spO2 = null)
        {
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications");
            var client = new RestClient(options);

            List<RestRequest> request = new();

            for(int i = 0; i <= relatives.Count + 1; i++)
            {
                request.Add(new RestRequest(""));
                request[i].AddHeader("Content-Type", "application/json");
                request[i].AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");
            }


            if (ENcontent.Contains("blood pressure"))
            {
                //Send Notification to Doctor
                request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"Systolic\":\"" + bloodPressure?.Systolic + "\",\"PulseRate\":\"" + bloodPressure?.PulseRate + "\", \"ImageLink\":\"" + bloodPressure?.ImageLink + "\", \"UpdatedDate\":\"" + bloodPressure?.Timestamp + "\",\"Indicator\":\"BloodPressure\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bloodPressure?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                await client.PostAsync(request[0]);

                //Send Notification to Relatives
                int j = 1;
                foreach (Person relative in relatives)
                {
                    request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"relativeId\",\"relation\":\"=\",\"value\":\"" + relative.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"Systolic\":\"" + bloodPressure?.Systolic + "\",\"PulseRate\":\"" + bloodPressure?.PulseRate + "\", \"ImageLink\":\"" + bloodPressure?.ImageLink + "\", \"UpdatedDate\":\"" + bloodPressure?.Timestamp + "\",\"Indicator\":\"BloodPressure\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bloodPressure?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                    await client.PostAsync(request[j]);
                    j++;
                }
            }
            else if (ENcontent.Contains("blood sugar"))
            {
                //Send Notification to Doctor
                request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"BloodSugar\":\"" + bloodSugar?.Value + "\",\"ImageLink\":\"" + bloodSugar?.ImageLink + "\", \"UpdatedDate\":\"" + bloodSugar?.Timestamp + "\",\"Indicator\":\"BloodSugar\",\"PatientName\":\""+patient.Name+"\"},\"large_icon\":\"" + bloodSugar?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                
                //Send Notification to Relatives
                int j = 1;
                foreach (Person relative in relatives)
                {
                    request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + relative.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"BloodSugar\":\"" + bloodSugar?.Value + "\",\"ImageLink\":\"" + bloodSugar?.ImageLink + "\", \"UpdatedDate\":\"" + bloodSugar?.Timestamp + "\",\"Indicator\":\"BloodSugar\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bloodSugar?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                    await client.PostAsync(request[j]);
                    j++;
                }
            }
            else if (ENcontent.Contains("body temperature"))
            {
                //Send Notification to Doctor
                request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"BodyTemperature\":\"" + bodyTemperature?.Value + "\",\"ImageLink\":\"" + bodyTemperature?.ImageLink + "\",\"UpdatedDate\":\"" + bodyTemperature?.Timestamp + "\",\"Indicator\":\"BodyTemperature\",\"PatientName\":\""+patient.Name+"\"},\"large_icon\":\"" + bodyTemperature?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                
                //Send Notification to Relatives
                int j = 1;
                foreach (Person relative in relatives)
                {
                    request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + relative.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"BodyTemperature\":\"" + bodyTemperature?.Value + "\",\"ImageLink\":\"" + bodyTemperature?.ImageLink + "\",\"UpdatedDate\":\"" + bodyTemperature?.Timestamp + "\",\"Indicator\":\"BodyTemperature\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bodyTemperature?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                    await client.PostAsync(request[j]);
                    j++;
                }
            }
            else if (ENcontent.Contains("SpO2"))
            {
                //Send Notification to Doctor
                request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"SpO2\":\"" + spO2?.Value + "\",\"ImageLink\":\"" + spO2?.ImageLink + "\",\"UpdatedDate\":\"" + spO2?.Timestamp + "\",\"Indicator\":\"SpO2\", \"PatientName\":\""+patient.Name+"\"},\"large_icon\":\"" + spO2?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                
                //Send Notification to Relatives
                int j = 1;
                foreach (Person relative in relatives)
                {
                    request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + relative.PersonId + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.PersonId + "\",\"SpO2\":\"" + spO2?.Value + "\",\"ImageLink\":\"" + spO2?.ImageLink + "\",\"UpdatedDate\":\"" + spO2?.Timestamp + "\",\"Indicator\":\"SpO2\", \"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + spO2?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                    await client.PostAsync(request[j]);
                    j++;
                }
            }
            //var notificationId = response.Content?.ToString().Substring(7, 36) ?? throw new ArgumentNullException("Creating notification unsucessfully");

            //Return Notification entity
            var notification = new Notification( VIcontent, DateTime.Now, patient, bloodPressure, bloodSugar, bodyTemperature , spO2, notificationType);
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
