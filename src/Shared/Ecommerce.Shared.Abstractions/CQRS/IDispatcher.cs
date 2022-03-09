namespace Ecommerce.Shared.Abstractions.CQRS;

public interface IDispatcher
{
    Task SendAsync<T>(T command) where T : class, ICommand;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
}