using Ecommerce.Order.Core.Aggregates;
using Ecommerce.Order.Core.Repositories;
using Ecommerce.Order.Shared.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Order.Consumers;

public class OrderFailedConsumer : IConsumer<OrderFailed>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderFailedConsumer> _logger;

    public OrderFailedConsumer(IOrderRepository orderRepository, ILogger<OrderFailedConsumer> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<OrderFailed> context)
    {
        _logger.LogInformation($"OrderFailed '{context.Message.OrderId}'");
        
        var order = await _orderRepository.Get(new OrderId(context.Message.OrderId));
        order.Fail(context.Message.Reason);

        await _orderRepository.Update(order);
    }
}