using Ecommerce.Order.Core.Aggregates;
using Ecommerce.Order.Core.Repositories;
#pragma warning disable CS8603

namespace Ecommerce.Order.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(Core.Aggregates.Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Core.Aggregates.Order order)
    {
        _dbContext.Orders.Update(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Core.Aggregates.Order> Get(OrderId orderId)
    {
        return await _dbContext.Orders.FindAsync(orderId);
    }
}