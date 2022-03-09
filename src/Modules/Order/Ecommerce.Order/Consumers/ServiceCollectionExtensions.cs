using Ecommerce.Order.Shared.Events;
using Ecommerce.Shared.Bus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Order.Consumers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderConsumers(this IServiceCollection serviceCollection)
    {
        BusConfiguration.AddQueueName<OrderPaid>("order-paid");
        BusConfiguration.AddQueueName<OrderFailed>("order-failed");
        
        BusConfiguration.AddBusConfigurator(busConfigurator =>
        {
            busConfigurator.AddConsumer<OrderPaidConsumer>();
            busConfigurator.AddConsumer<OrderFailedConsumer>();
        });
        
        BusConfiguration.AddBusFactoryConfigurator((busFactoryConfigurator, registration) =>
        {
            busFactoryConfigurator.ReceiveEndpoint("order-paid", e => e.ConfigureConsumer<OrderPaidConsumer>(registration));
            busFactoryConfigurator.ReceiveEndpoint("order-failed", e => e.ConfigureConsumer<OrderFailedConsumer>(registration));
        });

        return serviceCollection;
    }
}