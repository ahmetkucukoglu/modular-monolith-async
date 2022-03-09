namespace Ecommerce.Shared.Abstractions.CQRS;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
}