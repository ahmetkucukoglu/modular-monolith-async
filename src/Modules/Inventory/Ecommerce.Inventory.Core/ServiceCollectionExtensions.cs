using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Inventory.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryCore(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}