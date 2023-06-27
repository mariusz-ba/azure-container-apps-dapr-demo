using AzureContainerAppsDapr.Services.ServiceB.Messages;
using AzureContainerAppsDapr.Services.ServiceB.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureContainerAppsDapr.Services.ServiceB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationsController : ControllerBase
{
    private readonly IMessageBroker _messageBroker;

    public OperationsController(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    [HttpGet("operation-a")]
    public IActionResult OperationA() => Ok("operation-a");

    [HttpGet("operation-b")]
    public IActionResult OperationB() => Ok("operation-b");
    
    [HttpPost("operation-c")]
    public IActionResult OperationC() => Ok("operation-c");

    [HttpPost("operation-d")]
    public IActionResult OperationD() => Ok("operation-d");
    
    [HttpPost("operation-e")]
    public async Task<IActionResult> OperationE()
    {
        await _messageBroker.PublishAsync(new OperationEMessage(Guid.NewGuid(), "operation-e"));
        
        return Ok("operation-e");
    }

    [HttpPost("operation-f")]
    public async Task<IActionResult> OperationF()
    {
        await _messageBroker.PublishAsync(new OperationFMessage(Guid.NewGuid(), "operation-f"));
        
        return Ok("operation-f");
    }

    [HttpGet("operation-z")]
    public IActionResult OperationZ() => Ok(Url.ActionLink(nameof(OperationZ)));
}