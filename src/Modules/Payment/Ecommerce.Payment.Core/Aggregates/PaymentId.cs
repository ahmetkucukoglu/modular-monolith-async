using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Payment.Core.Aggregates;

public class PaymentId : EntityTypedId
{
    public PaymentId() : base()
    {
            
    }
    
    public PaymentId(Guid id) : base(id)
    {
        
    }
}