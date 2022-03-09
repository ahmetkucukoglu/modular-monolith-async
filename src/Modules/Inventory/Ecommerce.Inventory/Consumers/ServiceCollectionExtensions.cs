using Ecommerce.Inventory.Shared.Events;
using Ecommerce.Shared.Bus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Inventory.Consumers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryConsumers(this IServiceCollection serviceCollection)
    {
        BusConfiguration.AddQueueName<ReserveStock>("reserve-stock");
        BusConfiguration.AddQueueName<ReleaseStock>("release-stock");
        
        BusConfiguration.AddBusConfigurator(busConfigurator =>
        {
            busConfigurator.AddConsumer<ReserveStockConsumer>();
            busConfigurator.AddConsumer<ReleaseStockConsumer>();
        });
        
        BusConfiguration.AddBusFactoryConfigurator((busFactoryConfigurator, registration) =>
        {
            busFactoryConfigurator.ReceiveEndpoint("reserve-stock", e => e.ConfigureConsumer<ReserveStockConsumer>(registration));
            busFactoryConfigurator.ReceiveEndpoint("release-stock", e => e.ConfigureConsumer<ReleaseStockConsumer>(registration));
        });

        return serviceCollection;
    }
}