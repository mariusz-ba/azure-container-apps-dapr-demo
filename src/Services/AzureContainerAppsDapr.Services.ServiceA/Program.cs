using AzureContainerAppsDapr.Services.ServiceA.Services;
using AzureContainerAppsDapr.Shared.API.Networking;
using AzureContainerAppsDapr.Shared.Monitoring;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationInsightsMonitoring()
    .AddServiceClients()
    .AddControllers();

var app = builder.Build();

app.UseHeadersForwarding();

app.MapGet("/", () => "Service A");
app.MapControllers();

app.Run();