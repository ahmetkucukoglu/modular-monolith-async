namespace Ecommerce.Inventory.Shared.Events;

public record ReserveStock(Guid OrderId, IEnumerable<(string Sku, int Quantity)> Products)
{
    public Guid CorrelationId => OrderId;
}