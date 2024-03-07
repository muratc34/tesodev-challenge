namespace Shared.Core.Primitives.Result
{
    public class Result
    {
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, Error? error): this(isSuccess)
        {
            Error = error;
        }

        public bool IsSuccess { get; }
        public Error? Error { get; }
        public static Result Success() => new(true);
        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        public Result(T data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }
        public Result(T? data, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Data = data;
        }
        public T? Data { get; }

        public static Result<T> Success(T data) => new(data, true);
        public static Result<T> Failure(Error error, T? data) => new(data, true, error);
    }
}

