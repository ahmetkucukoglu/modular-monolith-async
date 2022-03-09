using Ecommerce.Inventory.Core.Repositories;
using Ecommerce.Inventory.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Inventory.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryInfrastructure(this  IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddDbContext<InventoryDbContext>(opt => opt.UseInMemoryDatabase("ecommerce-inventory"))
            .AddScoped<IInventoryRepository, InventoryRepository>();

        return serviceCollection;
    }
}