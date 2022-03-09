using Ecommerce.Inventory.Core.Repositories;
using Ecommerce.Inventory.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8603

namespace Ecommerce.Inventory.Infrastructure.Persistence;

public class InventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _dbContext;

    public InventoryRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Create(Core.Aggregates.Inventory inventory)
    {
        await _dbContext.Inventories.AddAsync(inventory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Core.Aggregates.Inventory inventory)
    {
        _dbContext.Inventories.Update(inventory);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<Core.Aggregates.Inventory> Get(ProductSku productSku)
    {
        return await _dbContext.Inventories.FirstOrDefaultAsync(i => i.Sku == productSku);
    }
}