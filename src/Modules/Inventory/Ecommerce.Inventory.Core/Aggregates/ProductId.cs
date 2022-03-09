using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Inventory.Core.Aggregates;

public class ProductId : EntityTypedId
{
    public ProductId() : base()
    {
            
    }
    
    public ProductId(Guid id) : base(id)
    {
        
    }
}