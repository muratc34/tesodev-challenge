﻿using FluentValidation.Results;
using Shared.Core.Primitives;

namespace Shared.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation failures has occurred.") =>
            Errors = failures
                .Distinct()
                .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
                .ToList();

        public IReadOnlyCollection<Error> Errors { get; }
    }
}
