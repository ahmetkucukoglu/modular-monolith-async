using Ecommerce.Payment.Core.Aggregates;

namespace Ecommerce.Payment.Core.Repositories;

public interface IPaymentRepository
{
    Task Create(Aggregates.Payment payment);
    Task Update(Core.Aggregates.Payment payment);
    Task<Aggregates.Payment> Get(PaymentId paymentId);
}