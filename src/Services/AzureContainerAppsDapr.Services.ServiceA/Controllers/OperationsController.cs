using Microsoft.AspNetCore.Mvc;

namespace AzureContainerAppsDapr.Services.ServiceA.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController : ControllerBase
{
    [HttpGet("operation-a")]
    public IActionResult OperationA() => Ok("operation-a");

    [HttpGet("operation-b")]
    public IActionResult OperationB() => Ok("operation-b");
    
    [HttpPost("operation-c")]
    public IActionResult OperationC() => Ok("operation-c");

    [HttpPost("operation-d")]
    public IActionResult OperationD() => Ok("operation-d");
    
    [HttpGet("operation-z")]
    public IActionResult OperationZ() => Ok(Url.ActionLink(nameof(OperationZ)));
}