
using Shared.Core.Primitives;

namespace Shared.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(Error error)
            : base(error.Message)
            => Error = error;

        public Error Error { get; }
    }
}
