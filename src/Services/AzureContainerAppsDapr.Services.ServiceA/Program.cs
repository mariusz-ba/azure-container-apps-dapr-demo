using AzureContainerAppsDapr.Shared.Monitoring;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationInsightsMonitoring()
    .AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Service A");
app.MapControllers();

app.Run();