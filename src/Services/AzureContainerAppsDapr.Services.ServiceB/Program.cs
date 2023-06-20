using AzureContainerAppsDapr.Shared.Monitoring;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationInsightsMonitoring();

var app = builder.Build();

app.MapGet("/", () => "Service B");

app.Run();