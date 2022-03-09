using Ecommerce.Payment.Core.Aggregates;
using Ecommerce.Payment.Core.Repositories;
#pragma warning disable CS8603

namespace Ecommerce.Payment.Infrastructure.Persistence;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _dbContext;
    
    public PaymentRepository(PaymentDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Create(Core.Aggregates.Payment payment)
    {
        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Update(Core.Aggregates.Payment payment)
    {
        _dbContext.Payments.Update(payment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Core.Aggregates.Payment> Get(PaymentId paymentId)
    {
        return await _dbContext.Payments.FindAsync(paymentId);
    }
}