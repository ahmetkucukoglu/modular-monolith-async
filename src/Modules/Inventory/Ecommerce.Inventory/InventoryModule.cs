using Ecommerce.Inventory.Core;
using Ecommerce.Inventory.Consumers;
using Ecommerce.Inventory.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Inventory;

public static class InventoryModule
{
    public static IServiceCollection AddInventory(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddInventoryInfrastructure()
            .AddInventoryCore()
            .AddInventoryConsumers();
        
        return serviceCollection;
    }
}