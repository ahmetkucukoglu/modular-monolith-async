namespace Ecommerce.Order.Shared.Events;

public record OrderReceived(Guid OrderId, decimal TotalPrice, string Currency, List<(string Sku, int Quantity)> Products)
{
    public Guid CorrelationId => OrderId;
}