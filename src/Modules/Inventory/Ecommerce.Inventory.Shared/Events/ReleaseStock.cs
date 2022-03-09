namespace Ecommerce.Inventory.Shared.Events;

public record ReleaseStock(Guid OrderId, List<(string Sku, int Quantity)> Products)
{
    public Guid CorrelationId => OrderId;
}