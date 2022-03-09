using Ecommerce.Inventory.Core.Repositories;
using Ecommerce.Shared.Abstractions.CQRS;
using Ecommerce.Shared.Abstractions.DDD;

namespace Ecommerce.Inventory.Core.Commands;

public record CreateInventory(string Sku, int Quantity) : ICommand;

public class CreateInventoryHandler : ICommandHandler<CreateInventory>
{
    private readonly IInventoryRepository _inventoryRepository;

    public CreateInventoryHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }
    
    public async Task HandleAsync(CreateInventory command)
    {
        var inventory = await _inventoryRepository.Get(new (command.Sku));

        if (inventory is not null)
            throw new DomainException("The inventory has already been added.");
        
        inventory = Core.Aggregates.Inventory.Create(new (command.Sku), command.Quantity);

        await _inventoryRepository.Create(inventory);
    }
}