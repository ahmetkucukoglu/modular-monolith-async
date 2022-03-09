using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Payment.Core.ValueObjects;

public class OrderId : EntityTypedId
{
    public OrderId() : base()
    {
        
    }
    
    public OrderId(Guid id) : base(id)
    {
        
    }
}