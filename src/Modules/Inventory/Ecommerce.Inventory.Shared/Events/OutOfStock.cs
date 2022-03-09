namespace Ecommerce.Inventory.Shared.Events;

public record OutOfStock(Guid OrderId, string Reason)
{
    public Guid CorrelationId => OrderId;
}