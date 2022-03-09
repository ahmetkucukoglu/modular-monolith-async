namespace Ecommerce.Order.Shared.Events;

public record OrderPaid(Guid OrderId)
{
    public Guid CorrelationId => OrderId;
}