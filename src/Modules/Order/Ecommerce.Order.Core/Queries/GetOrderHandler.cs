using Ecommerce.Order.Core.Repositories;
using Ecommerce.Shared.Abstractions.CQRS;

namespace Ecommerce.Order.Core.Queries;

public record GetOrder(Guid OrderId) : IQuery<GetOrderResponse>;
public record GetOrderResponse(Guid OrderId, string Status, string FailedReason, IEnumerable<GetOrderItemResponse> Items);
public record GetOrderItemResponse(Guid OrderItemId, string Sku, int Quantity, decimal UnitPrice, string Currency);

public class GetOrderHandler : IQueryHandler<GetOrder, GetOrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<GetOrderResponse> HandleAsync(GetOrder query)
    {
        var order = await _orderRepository.Get(new (query.OrderId));
        
        if (order is null)
            return null;

        var response = new GetOrderResponse(order.Id, order.Status.ToString(), order.FailedReason,
            order.Items.Select(i =>
                new GetOrderItemResponse(i.Id, i.Sku, i.Quantity, i.Price.Amount, i.Price.Currency)));

        return response;
    }
}