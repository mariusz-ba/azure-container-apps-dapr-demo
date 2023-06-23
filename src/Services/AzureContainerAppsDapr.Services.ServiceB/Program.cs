using AzureContainerAppsDapr.Services.ServiceB.Services;
using AzureContainerAppsDapr.Shared.API.Networking;
using AzureContainerAppsDapr.Shared.Monitoring;
using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationInsightsMonitoring()
    .AddSingleton(new DaprClientBuilder().Build())
    .AddSingleton<IMessageBroker, MessageBroker>()
    .AddControllers();

var app = builder.Build();

app.UseHeadersForwarding();

app.MapGet("/", () => "Service B");
app.MapControllers();

app.Run();