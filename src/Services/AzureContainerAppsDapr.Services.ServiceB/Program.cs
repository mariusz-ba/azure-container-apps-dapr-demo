using AzureContainerAppsDapr.Shared.API.Networking;
using AzureContainerAppsDapr.Shared.Monitoring;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationInsightsMonitoring()
    .AddControllers();

var app = builder.Build();

app.UseHeadersForwarding();

app.MapGet("/", () => "Service B");
app.MapControllers();

app.Run();