using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Order.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderCore(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}