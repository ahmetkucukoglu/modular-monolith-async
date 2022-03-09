using Ecommerce.Order.Core.Events;
using Ecommerce.Order.Core.ValueObjects;
using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Order.Core.Aggregates;

public class Order : AggregateRoot<OrderId>
{
    public string FailedReason { get; private set; } = "";
    public OrderStatuses Status { get; set; }

    public Price TotalPrice => new Price(
        Items.Sum(i => i.Price.Amount * i.Quantity),
        Items.FirstOrDefault()?.Price.Currency
    );

    private ICollection<OrderItem> _items = new List<OrderItem>();

    public ICollection<OrderItem> Items
    {
        get => _items;
        private set => _items = value;
    }

    protected Order() : base()
    {
    }

    private Order(OrderId id, IEnumerable<OrderItem> items) : base(id)
    {
        Id = id;
        Status = OrderStatuses.Created;

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    public static Order Create(OrderId id, IEnumerable<OrderItem> items)
    {
        var order = new Order(id, items);
        order.RaiseEvent(new OrderCreated(order));

        return order;
    }

    public void Pay()
    {
        Status = OrderStatuses.Paid;
    }

    public void Fail(string reason)
    {
        Status = OrderStatuses.Failed;
        FailedReason = reason;
    }
}