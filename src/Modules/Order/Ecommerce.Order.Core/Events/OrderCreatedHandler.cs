using Ecommerce.Order.Shared.Events;
using Ecommerce.Shared.Abstractions.DDD;
using Ecommerce.Shared.Abstractions.Bus;

namespace Ecommerce.Order.Core.Events;

public class OrderCreatedHandler : IDomainEventHandler<OrderCreated>
{
    private readonly IBusService _busService;

    public OrderCreatedHandler(IBusService busService)
    {
        _busService = busService;
    }
    public async Task HandleAsync(OrderCreated domainEvent)
    {
        await _busService.Send(
            new OrderReceived(
                domainEvent.Order.Id,
                domainEvent.Order.TotalPrice.Amount,
                domainEvent.Order.TotalPrice.Currency,
                domainEvent.Order.Items.Select(i => (i.Sku, i.Quantity)).ToList()));
    }
}