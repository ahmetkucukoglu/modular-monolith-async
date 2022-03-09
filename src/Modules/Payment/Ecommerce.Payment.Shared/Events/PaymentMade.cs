namespace Ecommerce.Payment.Shared.Events;

public record PaymentMade(Guid OrderId)
{
    public Guid CorrelationId => OrderId;
}