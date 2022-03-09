using System.Reflection;
using Ecommerce.Shared.Abstractions.DDD;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Shared.DDD;

public static class ServiceCollectionExtensions
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddDDD(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        serviceCollection.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        
        var handlers = assemblies
            .SelectMany(s => s.GetTypes())
            .Where(p => p.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDomainEventHandler<>)));

        foreach (var handler in handlers)
        {
            var @interface = handler.GetInterfaces()[0];
            serviceCollection.AddScoped(@interface,handler);
        }

        return serviceCollection;
    } 
}