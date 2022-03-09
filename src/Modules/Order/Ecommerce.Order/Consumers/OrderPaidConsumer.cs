using Ecommerce.Order.Core.Commands;
using Ecommerce.Order.Shared.Events;
using Ecommerce.Shared.Abstractions.CQRS;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Order.Consumers;

public class OrderPaidConsumer : IConsumer<OrderPaid>
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<OrderPaidConsumer> _logger;

    public OrderPaidConsumer(IDispatcher dispatcher, ILogger<OrderPaidConsumer> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<OrderPaid> context)
    {
        _logger.LogInformation($"OrderPaid '{context.Message.OrderId}'");

        await _dispatcher.SendAsync(new PayOrder(context.Message.OrderId));
    }
}