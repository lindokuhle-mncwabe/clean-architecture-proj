namespace GatheringEvents.Domain.Types;

public record Either<T, TError>
{
    public T? Value { get; }
    public TError? Error { get; }
    public bool IsUnhandledError { get; }

    private Either(T value, TError error, bool isUnhandledError)
    {
        Value = value;
        Error = error;
        IsUnhandledError = isUnhandledError;
    }

    public static Either<T, TError> Ok(T value) => new(value, default!, false);

    public static Either<T, TError> Fail(TError error, bool isUnhandledError) 
        => new(default!, error, isUnhandledError);
}