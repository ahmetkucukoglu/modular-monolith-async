using Ecommerce.Order.Core.ValueObjects;
using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Order.Core.Aggregates;

public class OrderItem : Entity<OrderItemId>
{
    public string Sku { get; }
    public Price Price { get; }
    public int Quantity { get; }

    protected OrderItem() : base(){}
    
    public OrderItem(OrderItemId id, string sku, int quantity, Price price)
    {
        Id = id;
        Sku = sku;
        Quantity = quantity;
        Price = price;
    }
}