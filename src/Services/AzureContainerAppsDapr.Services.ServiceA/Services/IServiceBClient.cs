namespace AzureContainerAppsDapr.Services.ServiceA.Services;

public interface IServiceBClient
{
    Task OperationCAsync();
    Task OperationDAsync();
}