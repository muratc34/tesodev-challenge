using MediatR;
using Shared.Core.Primitives.Result;

namespace Order.Application.Core.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
