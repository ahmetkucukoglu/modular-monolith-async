using Ecommerce.Order.Core.Repositories;
using Ecommerce.Shared.Abstractions.CQRS;

namespace Ecommerce.Order.Core.Commands;

public record PayOrder(Guid OrderId) : ICommand;

public class PayOrderHandler : ICommandHandler<PayOrder>
{
    private readonly IOrderRepository _orderRepository;

    public PayOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task HandleAsync(PayOrder command)
    {
        var order = await _orderRepository.Get(new (command.OrderId));
        order.Pay();

        await _orderRepository.Update(order);
    }
}