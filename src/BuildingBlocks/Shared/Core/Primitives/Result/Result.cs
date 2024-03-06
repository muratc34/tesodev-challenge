namespace Shared.Core.Primitives.Result
{
    public class Result
    {
        public Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        public Result(bool isSuccess, Error error, T data) : base(isSuccess, error)
        {
            Data = data;
        }
        public T Data { get; }

        public static Result<T> Success(T data) => new(true, Error.None, data);
        public static Result<T> Failure(Error error, T data) => new(false, error, data);
    }
}

