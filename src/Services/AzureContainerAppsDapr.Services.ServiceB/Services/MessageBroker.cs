using Dapr.Client;
using Dapr;

namespace AzureContainerAppsDapr.Services.ServiceB.Services;

public class MessageBroker : IMessageBroker
{
    private readonly DaprClient _daprClient;

    public MessageBroker(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task PublishAsync<TMessage>(TMessage message)
    {
        if (message is null)
        {
            return;
        }

        await _daprClient.PublishEventAsync("message-broker", "service-b", new CloudEvent<TMessage>(message)
        {
            Type = message.GetType().Name,
            Source = new Uri("service:service-b")
        });
    }
}