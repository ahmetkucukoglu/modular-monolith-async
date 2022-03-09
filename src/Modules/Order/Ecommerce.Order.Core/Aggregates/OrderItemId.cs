using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Order.Core.Aggregates;

public class OrderItemId : EntityTypedId
{
    public OrderItemId() : base()
    {
        
    }
    public OrderItemId(Guid id) : base(id)
    {
        
    }
}