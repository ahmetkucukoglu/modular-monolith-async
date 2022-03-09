using Ecommerce.Inventory.Core.Repositories;
using Ecommerce.Inventory.Shared.Events;
using Ecommerce.Shared.Abstractions.CQRS;
using Ecommerce.Shared.Abstractions.Bus;

namespace Ecommerce.Inventory.Core.Commands;

public record ReserveInventory(Guid OrderId, IEnumerable<(string Sku, int Quantity)> Products) : ICommand;

public class ReserveInventoryHandler : ICommandHandler<ReserveInventory>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IBusService _busService;

    public ReserveInventoryHandler(IInventoryRepository inventoryRepository, IBusService busService)
    {
        _inventoryRepository = inventoryRepository;
        _busService = busService;
    }

    public async Task HandleAsync(ReserveInventory command)
    {
        var outOfStock = new List<string>();

        foreach (var product in command.Products)
        {
            var inventory = await _inventoryRepository.Get(new (product.Sku));

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (inventory == null || inventory.Quantity < product.Quantity)
            {
                outOfStock.Add(product.Sku);
            }
        }

        if (outOfStock.Count > 0)
        {
            var reason = "Out of stock for product(s) " + outOfStock.Aggregate((sku1, sku2) => sku1 + "," + sku2);
            
            await _busService.Send(new OutOfStock(command.OrderId, reason));

            return;
        }

        foreach (var product in command.Products)
        {
            var inventory = await _inventoryRepository.Get(new (product.Sku));

            inventory.Reserve(product.Quantity);

            await _inventoryRepository.Update(inventory);
        }

        await _busService.Send(new StockReserved(command.OrderId));
    }
}