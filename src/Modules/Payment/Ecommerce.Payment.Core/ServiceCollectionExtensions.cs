using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Payment.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentCore(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}