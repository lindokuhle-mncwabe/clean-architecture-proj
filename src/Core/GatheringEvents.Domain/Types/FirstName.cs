using System;

namespace GatheringEvents.Domain.Types;

public record FirstName
{
    public string Value { get; set; }

    public FirstName(string value)
    {
        Value = value;
    }

    public static implicit operator FirstName(string value) => new (value);
    public override string ToString() => Value;
}