using Ecommerce.Payment.Core.ValueObjects;
using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Payment.Core.Aggregates;

public class Payment : AggregateRoot<PaymentId>
{
    public OrderId OrderId { get; private set; }
    public Price TotalPrice { get; private set; }
    public PaymentStatuses Status { get; private set; }
    public Guid TransactionId { get; private set; }
    public string FailedReason { get; private set; } = "";

    protected Payment() : base()
    {
    }

    private Payment(PaymentId id, OrderId orderId, Price totalPrice) : base(id)
    {
        Id = id;
        OrderId = orderId;
        TotalPrice = totalPrice;
        Status = PaymentStatuses.Created;
    }

    public static Payment Create(OrderId orderId, Price totalPrice)
    {
        var order = new Payment(new PaymentId(), orderId, totalPrice);

        return order;
    }

    public void Pay(Guid transactionId)
    {
        TransactionId = transactionId;
        Status = PaymentStatuses.Paid;
    }

    public void Fail(Guid transactionId, string reason)
    {
        TransactionId = transactionId;
        Status = PaymentStatuses.Failed;
        FailedReason = reason;
    }
}