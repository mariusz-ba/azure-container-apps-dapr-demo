using Microsoft.Extensions.DependencyInjection;

namespace AzureContainerAppsDapr.Shared.Monitoring;

public static class Extensions
{
    public static IServiceCollection AddApplicationInsightsMonitoring(this IServiceCollection services)
        => services.AddApplicationInsightsTelemetry();
}