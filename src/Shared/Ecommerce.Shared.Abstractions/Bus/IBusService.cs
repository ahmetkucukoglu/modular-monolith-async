namespace Ecommerce.Shared.Abstractions.Bus;

public interface IBusService
{
    Task Send<TEvent>(TEvent @event) where TEvent : class;
}