using AzureContainerAppsDapr.Services.ServiceA.Services;
using AzureContainerAppsDapr.Shared.API.Networking;
using AzureContainerAppsDapr.Shared.Monitoring;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationInsightsMonitoring();
builder.Services.AddDaprClient();
builder.Services.AddServiceClients();
builder.Services.AddAuthentication().AddDapr();
builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHeadersForwarding();
app.UseCloudEvents();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();