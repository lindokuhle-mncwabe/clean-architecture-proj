namespace GatheringEvents.Domain.Types;

public record class Error
{
    public string Message { get; }

    //public ErrorType ErrorType { get; }

    public Error(string message)
    {
        Message = message;
        //ErrorType = errorType;
    }

    public static implicit operator Error(string value) => new(value);

}