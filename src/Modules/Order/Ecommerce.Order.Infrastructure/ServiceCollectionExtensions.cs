using Ecommerce.Order.Core.Repositories;
using Ecommerce.Order.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Order.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddDbContext<OrderDbContext>(opt => opt.UseInMemoryDatabase("ecommerce-order"))
            .AddScoped<IOrderRepository, OrderRepository>();

        return serviceCollection;
    }
}