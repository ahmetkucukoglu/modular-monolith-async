using Ecommerce.Order.Core;
using Ecommerce.Order.Consumers;
using Ecommerce.Order.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Order;

public static class OrderModule
{
    public static IServiceCollection AddOrder(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddOrderInfrastructure()
            .AddOrderCore()
            .AddOrderConsumers();
        
        return serviceCollection;
    }
}