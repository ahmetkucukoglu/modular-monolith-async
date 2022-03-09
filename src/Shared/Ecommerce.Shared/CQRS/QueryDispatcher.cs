using Ecommerce.Shared.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Shared.CQRS;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;

    public QueryDispatcher(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task SendAsync<T>(T command) where T : class, ICommand
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (command is null)
        {
            return;
        }
        
        var handler = _serviceFactory.CreateScope().ServiceProvider.GetRequiredService<ICommandHandler<T>>();
        await handler.HandleAsync(command);
    }

    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = _serviceFactory.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
        if (method is null)
        {
            throw new InvalidOperationException($"Query handler for '{typeof(TResult).Name}' is invalid.");
        }

        return await (Task<TResult>)method.Invoke(handler, new object[] {query });
    }
}