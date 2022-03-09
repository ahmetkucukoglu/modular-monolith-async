using Ecommerce.Order.Core.Aggregates;

namespace Ecommerce.Order.Core.Repositories;

public interface IOrderRepository
{
    Task Create(Aggregates.Order order);
    Task Update(Aggregates.Order order);
    Task<Aggregates.Order> Get(OrderId orderId);
}