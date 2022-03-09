using Ecommerce.Payment.Core.ValueObjects;

namespace Ecommerce.Payment.Core.Services;

public interface IPaymentGateway
{
    (bool IsSuccess, Guid TransactionId, string ErrorMessage) Pay(Price totalPrice);
}