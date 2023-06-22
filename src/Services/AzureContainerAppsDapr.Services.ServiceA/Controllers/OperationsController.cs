using AzureContainerAppsDapr.Services.ServiceA.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureContainerAppsDapr.Services.ServiceA.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController : ControllerBase
{
    private readonly IServiceBClient _serviceBClient;

    public OperationsController(IServiceBClient serviceBClient)
    {
        _serviceBClient = serviceBClient;
    }

    [HttpGet("operation-a")]
    public IActionResult OperationA() => Ok("operation-a");

    [HttpGet("operation-b")]
    public IActionResult OperationB() => Ok("operation-b");
    
    [HttpPost("operation-c")]
    public async Task<IActionResult> OperationC()
    {
        await _serviceBClient.OperationCAsync();
        return Ok("operation-c");
    }

    [HttpPost("operation-d")]
    public async Task<IActionResult> OperationD()
    {
        await _serviceBClient.OperationDAsync();
        return Ok("operation-d");
    }

    [HttpGet("operation-z")]
    public IActionResult OperationZ() => Ok(Url.ActionLink(nameof(OperationZ)));
}