namespace Ecommerce.Payment.Shared.Events;

public record MakePayment(Guid OrderId, decimal TotalPrice, string Currency)
{
    public Guid CorrelationId => OrderId;
}