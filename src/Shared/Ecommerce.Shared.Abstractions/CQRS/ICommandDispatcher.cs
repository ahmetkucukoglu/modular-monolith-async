namespace Ecommerce.Shared.Abstractions.CQRS;

public interface ICommandDispatcher
{
    Task SendAsync<T>(T command) where T : class, ICommand;
}