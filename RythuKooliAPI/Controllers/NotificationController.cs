using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificationController()
    {
        _notificationService = new NotificationService();
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        var success = await _notificationService.SendPushNotification(
            request.DeviceToken,
            request.Title,
            request.Body,
            request.Type,
            request.WorkId
        );

        if (success)
            return Ok(new { message = "Notification sent successfully" });
        else
            return StatusCode(500, new { error = "Failed to send notification" });
    }
}

public class NotificationRequest
{
    public string DeviceToken { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Type { get; set; }

    public string WorkId { get; set; } = string.Empty;
}
