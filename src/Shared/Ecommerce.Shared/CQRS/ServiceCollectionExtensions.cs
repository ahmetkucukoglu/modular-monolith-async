using System.Reflection;
using Ecommerce.Shared.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Shared.CQRS;

public static class ServiceCollectionExtensions
{
    // ReSharper disable once InconsistentNaming
    public static IServiceCollection AddCQRS(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        serviceCollection.AddScoped<ICommandDispatcher, CommandDispatcher>();
        serviceCollection.AddScoped<IQueryDispatcher, QueryDispatcher>();
        serviceCollection.AddScoped<IDispatcher, DefaultDispatcher>();
        
        var commandHandlers = assemblies
            .SelectMany(s => s.GetTypes())
            .Where(p => p.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

        foreach (var handler in commandHandlers)
        {
            var @interface = handler.GetInterfaces()[0];
            serviceCollection.AddScoped(@interface,handler);
        }
        
        var queryHandlers = assemblies
            .SelectMany(s => s.GetTypes())
            .Where(p => p.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

        foreach (var handler in queryHandlers)
        {
            var @interface = handler.GetInterfaces()[0];
            serviceCollection.AddScoped(@interface,handler);
        }
        
        return serviceCollection;
    }
}