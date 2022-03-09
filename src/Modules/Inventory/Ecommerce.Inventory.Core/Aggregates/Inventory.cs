using Ecommerce.Inventory.Core.ValueObjects;
using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Inventory.Core.Aggregates;

public class Inventory : AggregateRoot<ProductId>
{
    public ProductSku Sku { get; private set; }
    public int Quantity { get; private set; }
    
    protected Inventory() : base()
    {
        
    }

    private Inventory(ProductId productId, ProductSku sku, int quantity)
    {
        Id = productId;
        Sku = sku;
        Quantity = quantity;
    }

    public static Inventory Create(ProductSku sku, int quantity)
    {
        var inventory = new Inventory(new ProductId(), sku, quantity);

        return inventory;
    }

    public void Release(int quantity)
    {
        Quantity += quantity;
    }

    public void Reserve(int quantity)
    {
        Quantity -= quantity;
    }
}