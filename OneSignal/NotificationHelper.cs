//using HealthCareApplication.Migrations;
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
        public NotificationHelper()
        {
        }

        public async Task<List<Notification>> PushAsync(Person patient, Person doctor, List<Person> relatives, string VIcontent, string ENcontent, ENotificationType notificationType, BloodPressure? bloodPressure = null, BloodSugar? bloodSugar = null, BodyTemperature? bodyTemperature = null, SpO2? spO2 = null)
        {
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications");
            var client = new RestClient(options);

            List<RestRequest> request = new();
            List<Notification> notifications = new();
            List<RestResponse> responses = new();

            for (int i = 0; i <= relatives.Count; i++)
            {
                request.Add(new RestRequest(""));
                request[i].AddHeader("Content-Type", "application/json");
                request[i].AddHeader("Authorization", "Basic ZDViYWUzNGYtZjI3OS00N2Q3LWIwNDEtOWRjOGE3ZTQxOTVh");
            }

            if (ENcontent.Contains("blood pressure"))
            {
                responses = await PushNotificationsBloodPressure(patient, doctor, relatives, VIcontent, ENcontent, notificationType, options, client, request, bloodPressure);
            }
            if (ENcontent.Contains("blood sugar"))
            {
                responses = await PushNotificationsBloodSugar(patient, doctor, relatives, VIcontent, ENcontent, notificationType, options, client, request, bloodSugar);
            }
            if (ENcontent.Contains("body temperature"))
            {
                responses = await PushNotificationsBodyTemperature(patient, doctor, relatives, VIcontent, ENcontent, notificationType, options, client, request, bodyTemperature);
            }
            if (ENcontent.Contains("SpO2"))
            {
                responses = await PushNotificationsSpO2(patient, doctor, relatives, VIcontent, ENcontent, notificationType, options, client, request, spO2);
            }


            //Create Notification entities
            List<Person> carers = new();
            carers.Add(doctor);
            foreach (Person relative in relatives)
            {
                carers.Add(relative);
            }

            int n = 0;
            foreach (var response in responses)
            {
                if (!response.Content.Contains("error"))
                {
                    var notificationId = response.Content?.ToString().Substring(7, 36) ?? throw new ArgumentNullException("NotificationId received form Onesignal is null");
                    notifications.Add(new Notification(notificationId, VIcontent, DateTime.Now, patient.Id, patient.Name, carers[n], bloodPressure, bloodSugar, bodyTemperature, spO2, notificationType));
                    n++;
                }
                if (response.Content.Contains("error"))
                {
                    var notificationId = Guid.NewGuid().ToString("P").Substring(1,36);
                    notifications.Add(new Notification(notificationId, VIcontent, DateTime.Now, patient.Id, patient.Name, carers[n], bloodPressure, bloodSugar, bodyTemperature, spO2, notificationType));
                    n++;
                }
            }

            return notifications;
        }
        private async Task<List<RestResponse>> PushNotificationsBloodPressure(Person patient, Person doctor, List<Person> relatives, string VIcontent, string ENcontent, ENotificationType notificationType, RestClientOptions options, RestClient client, List<RestRequest> request, BloodPressure? bloodPressure)
        {
            List<RestResponse> responses = new();

            //Send Notification to Doctor
            request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"Systolic\":\"" + bloodPressure?.Systolic + "\",\"PulseRate\":\"" + bloodPressure?.PulseRate + "\", \"ImageLink\":\"" + bloodPressure?.ImageLink + "\", \"UpdatedDate\":\"" + bloodPressure?.Timestamp + "\",\"Indicator\":\"BloodPressure\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bloodPressure?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
            responses.Add(await client.PostAsync(request[0]));

            //Send Notification to Relatives
            int j = 1;
            foreach (Person relative in relatives)
            {
                request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"relativeId\",\"relation\":\"=\",\"value\":\"" + relative.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"Systolic\":\"" + bloodPressure?.Systolic + "\",\"PulseRate\":\"" + bloodPressure?.PulseRate + "\", \"ImageLink\":\"" + bloodPressure?.ImageLink + "\", \"UpdatedDate\":\"" + bloodPressure?.Timestamp + "\",\"Indicator\":\"BloodPressure\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bloodPressure?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                responses.Add(await client.PostAsync(request[j]));
                j++;
            }
            return responses;
        }
        private async Task<List<RestResponse>> PushNotificationsBloodSugar(Person patient, Person doctor, List<Person> relatives, string VIcontent, string ENcontent, ENotificationType notificationType, RestClientOptions options, RestClient client, List<RestRequest> request, BloodSugar? bloodSugar)
        {
            List<RestResponse> responses = new();

            //Send Notification to Doctor
            request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"BloodSugar\":\"" + bloodSugar?.Value + "\",\"ImageLink\":\"" + bloodSugar?.ImageLink + "\", \"UpdatedDate\":\"" + bloodSugar?.Timestamp + "\",\"Indicator\":\"BloodSugar\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bloodSugar?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
            responses.Add(await client.PostAsync(request[0]));

            //Send Notification to Relatives
            int j = 1;
            foreach (Person relative in relatives)
            {
                request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"relativeId\",\"relation\":\"=\",\"value\":\"" + relative.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"BloodSugar\":\"" + bloodSugar?.Value + "\",\"ImageLink\":\"" + bloodSugar?.ImageLink + "\", \"UpdatedDate\":\"" + bloodSugar?.Timestamp + "\",\"Indicator\":\"BloodSugar\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bloodSugar?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                responses.Add(await client.PostAsync(request[j]));
                j++;
            }

            return responses;
        }
        private async Task<List<RestResponse>> PushNotificationsBodyTemperature(Person patient, Person doctor, List<Person> relatives, string VIcontent, string ENcontent, ENotificationType notificationType, RestClientOptions options, RestClient client, List<RestRequest> request, BodyTemperature? bodyTemperature)
        {
            List<RestResponse> responses = new();

            //Send Notification to Doctor
            request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"BodyTemperature\":\"" + bodyTemperature?.Value + "\",\"ImageLink\":\"" + bodyTemperature?.ImageLink + "\",\"UpdatedDate\":\"" + bodyTemperature?.Timestamp + "\",\"Indicator\":\"BodyTemperature\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bodyTemperature?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
            responses.Add(await client.PostAsync(request[0]));

            //Send Notification to Relatives
            int j = 1;
            foreach (Person relative in relatives)
            {
                request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"relativeId\",\"relation\":\"=\",\"value\":\"" + relative.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"BodyTemperature\":\"" + bodyTemperature?.Value + "\",\"ImageLink\":\"" + bodyTemperature?.ImageLink + "\",\"UpdatedDate\":\"" + bodyTemperature?.Timestamp + "\",\"Indicator\":\"BodyTemperature\",\"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + bodyTemperature?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                responses.Add(await client.PostAsync(request[j]));
                j++;
            }
            return responses;
        }

        private async Task<List<RestResponse>> PushNotificationsSpO2(Person patient, Person doctor, List<Person> relatives, string VIcontent, string ENcontent, ENotificationType notificationType, RestClientOptions options, RestClient client, List<RestRequest> request, SpO2? spO2)
        {
            List<RestResponse> responses = new();

            //Send Notification to Doctor
            request[0].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"doctorId\",\"relation\":\"=\",\"value\":\"" + doctor.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"SpO2\":\"" + spO2?.Value + "\",\"ImageLink\":\"" + spO2?.ImageLink + "\",\"UpdatedDate\":\"" + spO2?.Timestamp + "\",\"Indicator\":\"SpO2\", \"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + spO2?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
            responses.Add(await client.PostAsync(request[0]));

            //Send Notification to Relatives
            int j = 1;
            foreach (Person relative in relatives)
            {
                request[j].AddJsonBody("{\"filters\":[{\"field\":\"tag\", \"key\":\"relativeId\",\"relation\":\"=\",\"value\":\"" + relative.Id + "\"}],\"contents\":{\"en\":\"" + ENcontent + "\",\"vi\":\"" + VIcontent + "\"},\"app_id\":\"eb1e614e-54fe-4824-9c1a-aad236ec92d3\",\"data\":{\"patientId\":\"" + patient.Id + "\",\"SpO2\":\"" + spO2?.Value + "\",\"ImageLink\":\"" + spO2?.ImageLink + "\",\"UpdatedDate\":\"" + spO2?.Timestamp + "\",\"Indicator\":\"SpO2\", \"PatientName\":\"" + patient.Name + "\"},\"large_icon\":\"" + spO2?.ImageLink + "\",\"android_channel_id\": \"e757239a-9b22-4630-9201-6bb51fd86a2f\"}", false);
                responses.Add(await client.PostAsync(request[j]));
                j++;
            }

            return responses;
        }
    }
}
