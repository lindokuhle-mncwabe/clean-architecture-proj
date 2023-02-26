namespace GatheringEvents.Domain.Types;

public sealed record Email
{
    public string Value { get; }
    public Email(string email)
    {
        Value = email;
    }
    
    public static implicit operator Email(string value) => new (value);
}