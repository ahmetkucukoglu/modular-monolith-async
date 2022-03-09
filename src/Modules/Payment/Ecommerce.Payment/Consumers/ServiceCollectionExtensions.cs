using Ecommerce.Payment.Shared.Events;
using Ecommerce.Shared.Bus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Payment.Consumers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentConsumers(this IServiceCollection serviceCollection)
    {
        BusConfiguration.AddQueueName<MakePayment>("make-payment");
        
        BusConfiguration.AddBusConfigurator(busConfigurator =>
        {
            busConfigurator.AddConsumer<MakePaymentConsumer>();
        });
        
        BusConfiguration.AddBusFactoryConfigurator((busFactoryConfigurator, registration) =>
        {
            busFactoryConfigurator.ReceiveEndpoint("make-payment", e => e.ConfigureConsumer<MakePaymentConsumer>(registration));
        });

        return serviceCollection;
    }
}