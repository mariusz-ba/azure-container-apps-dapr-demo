using AzureContainerAppsDapr.Services.ServiceB.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzureContainerAppsDapr.Services.ServiceB.Controllers;

[Authorize(AuthenticationSchemes = "Dapr")]
[ApiController]
[Route("dapr/subscriptions")]
public class DaprSubscriptionsController : ControllerBase
{
    private readonly ILogger<DaprSubscriptionsController> _logger;

    public DaprSubscriptionsController(ILogger<DaprSubscriptionsController> logger)
    {
        _logger = logger;
    }

    [HttpPost("operation-e")]
    public IActionResult OperationE([FromBody] OperationEMessage message)
    {
        _logger.LogInformation("OperationEMessage | Id: {Id}, Content: {Content}", message.Id, message.Content);
        
        return Ok();
    }
    
    [HttpPost("operation-f")]
    public IActionResult OperationF([FromBody] OperationFMessage message)
    {
        _logger.LogInformation("OperationFMessage | Id: {Id}, Content: {Content}", message.Id, message.Content);
        
        return Ok();
    }
}