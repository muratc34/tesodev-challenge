using MediatR;
using Shared.Core.Primitives.Result;

namespace Order.Application.Core.Messaging
{
    public interface ICommandHandler<TCommand> 
        : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<TCommand, TResponse>
        : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }
}
