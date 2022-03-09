namespace Ecommerce.Payment.Shared.Events;

public record PaymentFailed(Guid OrderId, string Reason)
{
    public Guid CorrelationId => OrderId;
}