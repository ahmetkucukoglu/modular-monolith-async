using Ecommerce.Payment.Core.Services;
using Ecommerce.Payment.Core.ValueObjects;

namespace Ecommerce.Payment.Infrastructure.Services;

public class PaymentGateway : IPaymentGateway
{
    public (bool IsSuccess, Guid TransactionId, string ErrorMessage) Pay(Price totalPrice)
    {
        var transactionId = Guid.NewGuid();
        
        return totalPrice.Amount >= 100 ? (false, transactionId, "Insufficient fund") : (true, transactionId, string.Empty);
    }
}