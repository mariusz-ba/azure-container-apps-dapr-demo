namespace AzureContainerAppsDapr.Services.ServiceA.Services;

public class ServiceBClient : IServiceBClient
{
    private readonly HttpClient _httpClient;

    public ServiceBClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task OperationCAsync()
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "/api/operations/operation-c");
        var response = await _httpClient.SendAsync(message);
        response.EnsureSuccessStatusCode();
    }

    public async Task OperationDAsync()
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "/api/operations/operation-d");
        var response = await _httpClient.SendAsync(message);
        response.EnsureSuccessStatusCode();
    }

    public async Task OperationEAsync()
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "/api/operations/operation-e");
        var response = await _httpClient.SendAsync(message);
        response.EnsureSuccessStatusCode();
    }

    public async Task OperationFAsync()
    {
        var message = new HttpRequestMessage(HttpMethod.Post, "/api/operations/operation-f");
        var response = await _httpClient.SendAsync(message);
        response.EnsureSuccessStatusCode();
    }
}