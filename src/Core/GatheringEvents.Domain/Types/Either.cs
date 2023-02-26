namespace GatheringEvents.Domain.Types;

public sealed record Either<T, TError>
{
    public T? Value { get; }
    public TError? Error { get; }

    private Either(T value, TError error)
    {
        Value = value;
        Error = error;
    }

    public static Either<T, TError> Ok(T value) => new(value, default!);

    public static Either<T, TError> Fail(TError error) 
        => new(default!, error);
}