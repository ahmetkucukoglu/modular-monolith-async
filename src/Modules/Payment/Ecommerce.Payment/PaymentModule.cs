using Ecommerce.Payment.Consumers;
using Ecommerce.Payment.Core;
using Ecommerce.Payment.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Payment;

public static class PaymentModule
{
    public static IServiceCollection AddPayment(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddPaymentInfrastructure()
            .AddPaymentCore()
            .AddPaymentConsumers();
        
        return serviceCollection;
    }
}