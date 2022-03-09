namespace Ecommerce.Shared.Abstractions.DDD;

public interface IDomainEventHandler<in T> where T : IDomainEvent
{
    Task HandleAsync(T domainEvent);
}