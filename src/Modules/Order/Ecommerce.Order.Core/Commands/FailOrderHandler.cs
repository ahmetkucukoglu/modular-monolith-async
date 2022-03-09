using Ecommerce.Order.Core.Repositories;
using Ecommerce.Shared.Abstractions.CQRS;

namespace Ecommerce.Order.Core.Commands;

public record FailOrder(Guid OrderId, string Reason) : ICommand;

public class FailOrderHandler : ICommandHandler<FailOrder>
{
    private readonly IOrderRepository _orderRepository;

    public FailOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task HandleAsync(FailOrder command)
    {
        var order = await _orderRepository.Get(new (command.OrderId));
        order.Fail(command.Reason);

        await _orderRepository.Update(order);
    }
}