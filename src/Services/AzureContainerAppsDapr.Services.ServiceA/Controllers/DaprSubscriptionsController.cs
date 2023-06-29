using AzureContainerAppsDapr.Services.ServiceA.Messages;
using Dapr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzureContainerAppsDapr.Services.ServiceA.Controllers;

[TopicMetadata("routingKey", "OperationEMessage,OperationFMessage")]
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

    [Topic("message-broker", "service-b", "event.type == \"OperationEMessage\"", 1)]
    [HttpPost("operation-e")]
    public IActionResult OperationE([FromBody] OperationEMessage message)
    {
        _logger.LogInformation("OperationEMessage | Id: {Id}, Content: {Content}", message.Id, message.Content);
        
        return Ok();
    }
    
    [Topic("message-broker", "service-b", "event.type == \"OperationFMessage\"", 1)]
    [HttpPost("operation-f")]
    public IActionResult OperationF([FromBody] OperationFMessage message)
    {
        _logger.LogInformation("OperationFMessage | Id: {Id}, Content: {Content}", message.Id, message.Content);
        
        return Ok();
    }
}