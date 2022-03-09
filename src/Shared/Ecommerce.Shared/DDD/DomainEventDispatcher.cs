using Ecommerce.Shared.Abstractions.DDD;
using Microsoft.Extensions.DependencyInjection;
#pragma warning disable CS8620

namespace Ecommerce.Shared.DDD;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task DispatchAsync(params IDomainEvent[] events)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (events is null || !events.Any())
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        foreach (var @event in events)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);
                
            var tasks = handlers.Select(x => (Task) handlerType
                .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                ?.Invoke(x, new object?[] {@event}));
                
            await Task.WhenAll(tasks);
        }
    }
}