using MediatR;
using Shared.Core.Primitives.Result;

namespace Order.Application.Core.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
