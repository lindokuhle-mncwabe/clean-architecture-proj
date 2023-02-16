using System;

namespace GatheringEvents.Domain.Types;

public record CreatedOnUtc
{
    public DateTime Value { get; set; }
    public CreatedOnUtc(DateTime value)
    {
        Value = value;
    }

    public static implicit operator CreatedOnUtc(DateTime value) => new (value);
    public override string ToString() => Value.ToString("u"); 
}