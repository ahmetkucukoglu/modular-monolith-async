using Ecommerce.Shared.Abstractions.CQRS;

namespace Ecommerce.Shared.CQRS;

public class DefaultDispatcher : IDispatcher
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public DefaultDispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    public Task SendAsync<T>(T command) where T : class, ICommand
        => _commandDispatcher.SendAsync(command);
    
    public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        => _queryDispatcher.QueryAsync(query);
}