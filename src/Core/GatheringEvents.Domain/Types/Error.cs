namespace GatheringEvents.Domain.Types;

public record class Error
{
    public string Message { get; }

    public Error(string message)
    {
        Message = message;
    }

    public static implicit operator Error(string value) => new(value);

}