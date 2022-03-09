using Ecommerce.Shared.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Shared.CQRS;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;

    public CommandDispatcher(IServiceScopeFactory serviceFactory)
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
}