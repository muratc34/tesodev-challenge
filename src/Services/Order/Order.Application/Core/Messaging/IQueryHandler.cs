using MediatR;
using Shared.Core.Primitives.Result;

namespace Order.Application.Core.Messaging
{
    public interface IQueryHandler<TQuery, TResponse>
        : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
