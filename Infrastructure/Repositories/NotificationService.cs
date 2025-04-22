using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

public class NotificationService : INotificationService
{
    private readonly string _projectId;
    private readonly string _jsonPath;

    public NotificationService()
    {
        _projectId = "rythukooli";  // 🔹 Replace with your Firebase Project ID
        _jsonPath = @"D:\RythuKooli\rythukooli-4185ab892f72.json";  // 🔹 Update with actual JSON file path
    }

    public async Task<bool> SendPushNotification(string deviceToken, string title, string body, string type, string workId)
    {
        try
        {
            var credential = GoogleCredential.FromFile(_jsonPath)
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var fcmUrl = $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send";

            var payload = new
            {
                message = new
                {
                    token = deviceToken,
                    notification = new
                    {
                        title = title,
                        body = body
                    },
                    data = new
                    {
                        click_action = "FLUTTER_NOTIFICATION_CLICK",
                        type = type,
                        workId = workId
                    }
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(fcmUrl, content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending FCM notification: {ex.Message}");
            return false;
        }
    }
}
