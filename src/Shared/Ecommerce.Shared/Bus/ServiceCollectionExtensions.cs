using Ecommerce.Shared.Abstractions.Bus;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Shared.Bus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBus(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBusService, BusService>();
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            BusConfiguration.GetBusConfigurators()(busConfigurator);

            busConfigurator.AddBus(registrationContext =>
                MassTransit.Bus.Factory.CreateUsingInMemory(busFactoryConfigurator =>
                {
                    BusConfiguration.GetBusFactoryConfigurators()(busFactoryConfigurator, registrationContext);
                })
            );
        });
        
        serviceCollection.AddMassTransitHostedService();
        
        return serviceCollection;
    }
}