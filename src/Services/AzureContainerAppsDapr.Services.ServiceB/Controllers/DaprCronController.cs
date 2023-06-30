using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzureContainerAppsDapr.Services.ServiceB.Controllers;

[Authorize(AuthenticationSchemes = "Dapr")]
[ApiController]
[Route("dapr/cron")]
public class DaprCronController : ControllerBase
{
    private readonly ILogger<DaprCronController> _logger;

    public DaprCronController(ILogger<DaprCronController> logger)
    {
        _logger = logger;
    }

    [HttpOptions("send-notification")]
    public IActionResult SendNotificationOptions() => Ok();
    
    [HttpPost("send-notification")]
    public IActionResult SendNotification()
    {
        _logger.LogInformation("Sending notification: {DateTimeUtc}", DateTime.UtcNow);

        return Ok();
    }
}