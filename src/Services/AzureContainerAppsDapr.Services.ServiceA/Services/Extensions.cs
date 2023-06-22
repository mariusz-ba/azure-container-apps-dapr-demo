using System.Net.Http.Headers;

namespace AzureContainerAppsDapr.Services.ServiceA.Services;

public static class Extensions
{
    public static IServiceCollection AddServiceClients(this IServiceCollection services)
    {
        services.AddHttpClient<IServiceBClient, ServiceBClient>(httpClient =>
        {
            httpClient.BaseAddress = new Uri($"http://localhost:{Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500"}/");
            httpClient.DefaultRequestHeaders.Add("dapr-app-id", "aca-demo-service-b");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
        
        return services;
    }
}