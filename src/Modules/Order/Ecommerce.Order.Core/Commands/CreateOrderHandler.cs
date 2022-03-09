using Ecommerce.Order.Core.Aggregates;
using Ecommerce.Order.Core.Repositories;
using Ecommerce.Shared.Abstractions.CQRS;
using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Order.Core.Commands;

public record CreateOrder(Guid OrderId, IEnumerable<CreateOrderItem> Items) : ICommand;

public record CreateOrderItem(Guid OrderItemId, string Sku, int Quantity, decimal UnitPrice, string Currency);

public class CreateOrderHandler : ICommandHandler<CreateOrder>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task HandleAsync(CreateOrder command)
    {
        var order = await _orderRepository.Get(new (command.OrderId));
        
        if (order is not null)
            throw new DomainException("The order has already been added.");
        
        order = Order.Core.Aggregates.Order.Create(
            new (command.OrderId),
            command.Items.Select(i =>
                new OrderItem(new (i.OrderItemId), i.Sku, i.Quantity, new (i.UnitPrice, i.Currency))
            ));

        await _orderRepository.Create(order);
    }
}