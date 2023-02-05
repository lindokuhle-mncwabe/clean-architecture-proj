using System;

namespace GatheringEvents.Domain.Types;

public record FullName
{
    public string Value { get; set; }

    public FullName(string value)
    {
        Value = value;
    }

    public static implicit operator FullName(string value) => new (value);
    public override string ToString() => Value;
}