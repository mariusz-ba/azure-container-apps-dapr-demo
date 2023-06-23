using AzureContainerAppsDapr.Services.ServiceA.Services;
using AzureContainerAppsDapr.Shared.API.Networking;
using AzureContainerAppsDapr.Shared.Monitoring;
using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationInsightsMonitoring()
    .AddServiceClients()
    .AddSingleton(new DaprClientBuilder().Build())
    .AddControllers();

var app = builder.Build();

app.UseHeadersForwarding();
app.UseCloudEvents();

app.MapGet("/", () => "Service A");
app.MapSubscribeHandler();
app.MapControllers();

app.Run();