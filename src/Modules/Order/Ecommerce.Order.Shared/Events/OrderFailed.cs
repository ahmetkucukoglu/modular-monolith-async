namespace Ecommerce.Order.Shared.Events;

public record OrderFailed(Guid OrderId, string Reason)
{
    public Guid CorrelationId => OrderId;
}