

using Shared.Core.Primitives;

namespace Shared.Contracts
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;
        public IReadOnlyCollection<Error> Errors { get; }
    }
}
