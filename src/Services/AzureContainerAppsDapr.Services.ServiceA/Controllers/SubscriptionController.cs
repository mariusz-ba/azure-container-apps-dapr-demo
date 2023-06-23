using AzureContainerAppsDapr.Services.ServiceA.Messages;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace AzureContainerAppsDapr.Services.ServiceA.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ILogger<SubscriptionController> _logger;

    public SubscriptionController(ILogger<SubscriptionController> logger)
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