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

        var data = new CloudEvent<TMessage>(message)
        {
            Type = message.GetType().Name,
            Source = new Uri("service:service-b")
        };

        var metadata = new Dictionary<string, string>
        {
            { "routingKey", data.Type }
        };

        await _daprClient.PublishEventAsync("message-broker", "service-b", data, metadata);
    }
}