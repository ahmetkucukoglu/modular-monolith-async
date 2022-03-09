using Ecommerce.Shared.Abstractions.Bus;
using MassTransit;

namespace Ecommerce.Shared.Bus;

public class BusService : IBusService
{
    private readonly ISendEndpointProvider _sendEndpointProvider; 

    public BusService(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Send<TEvent>(TEvent @event) where TEvent : class
    {
        var queueName = BusConfiguration.GetQueueName<TEvent>();
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new(queueName));

        await endpoint.Send(@event);
    }
}