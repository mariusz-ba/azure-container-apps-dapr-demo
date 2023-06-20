using AzureContainerAppsDapr.Shared.Monitoring;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationInsightsMonitoring()
    .AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Service B");
app.MapControllers();

app.Run();