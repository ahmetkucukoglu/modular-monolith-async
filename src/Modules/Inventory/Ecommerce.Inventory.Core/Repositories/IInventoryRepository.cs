using Ecommerce.Inventory.Core.ValueObjects;

namespace Ecommerce.Inventory.Core.Repositories;

public interface IInventoryRepository
{
    Task Create(Aggregates.Inventory inventory);
    Task Update(Aggregates.Inventory inventory);
    Task<Aggregates.Inventory> Get(ProductSku productSku);
}