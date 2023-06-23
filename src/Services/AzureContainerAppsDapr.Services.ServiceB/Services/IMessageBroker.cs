namespace AzureContainerAppsDapr.Services.ServiceB.Services;

public interface IMessageBroker
{
    Task PublishAsync<TMessage>(TMessage message);
}