namespace Ecommerce.Shared.Abstractions.DDD;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(params IDomainEvent[] events);
}