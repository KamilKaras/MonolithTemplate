namespace MonolithTemplate.Shared.Results;

public class Result
{
    protected Result()
    {
        IsSuccess = true;
        Error = default;
    }
    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    public Error? Error { get; }
    public bool IsSuccess { get; }

    public static Result Success() =>
        new();

    public static Result Failure(Error error) =>
        new(error);
}

public sealed class Result<T> : Result
{
    private readonly T? _value;
    private Result(T value) : base()
    {
        _value = value;
    }
    private Result(Error error) : base(error)
    {
        _value = default;
    }

    public T Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("Brak dostępu do wartości jeżeli!");

    public static Result<T> Success(T value) =>
        new(value);

    public static new Result<T> Failure(Error error) =>
        new(error);
}
