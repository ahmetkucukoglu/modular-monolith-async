namespace Ecommerce.Payment.Shared.Services;

public interface IPaymentService
{
    Task<bool> MakePayment(Guid OrderId, decimal TotalPrice, string Currency);
}