namespace Ecommerce.Shared.Abstractions.CQRS;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command);
}