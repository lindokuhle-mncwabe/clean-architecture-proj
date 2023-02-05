namespace GatheringEvents.Domain.Types;

public record Result<T, TError>
{
    public T Value { get; }
    public TError Error { get; }
    public bool IsSuccess { get; }
    public bool IsUnhandledError { get; }

    private Result(T value, TError error, bool isSuccess, bool isUnhandledError)
    {
        Value = value;
        Error = error;
        IsSuccess = isSuccess;
        IsUnhandledError = isUnhandledError;
    }

    public static Result<T, TError> Ok(T value) => new(value, default!, true, false);

    public static Result<T, TError> Fail(TError error, bool isUnhandledError) 
        => new(default!, error, false, isUnhandledError);
}