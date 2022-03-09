namespace Ecommerce.Inventory.Shared.Events;

public record StockReserved(Guid OrderId)
{
    public Guid CorrelationId => OrderId;
}