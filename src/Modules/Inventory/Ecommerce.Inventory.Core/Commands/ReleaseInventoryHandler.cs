using Ecommerce.Inventory.Core.Repositories;
using Ecommerce.Shared.Abstractions.CQRS;
using Ecommerce.Shared.Abstractions.Bus;

namespace Ecommerce.Inventory.Core.Commands;

public record ReleaseInventory(IEnumerable<(string Sku, int Quantity)> Products) : ICommand;

public class ReleaseInventoryHandler : ICommandHandler<ReleaseInventory>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IBusService _busService;

    public ReleaseInventoryHandler(IInventoryRepository inventoryRepository, IBusService busService)
    {
        _inventoryRepository = inventoryRepository;
        _busService = busService;
    }
    
    public async Task HandleAsync(ReleaseInventory command)
    {
        foreach (var product in command.Products)
        {
            var inventory = await _inventoryRepository.Get(new (product.Sku));
            inventory.Release(product.Quantity);

            await _inventoryRepository.Update(inventory);
        }
    }
}