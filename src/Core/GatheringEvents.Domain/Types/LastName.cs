using System;

namespace GatheringEvents.Domain.Types;

public sealed record LastName
{
    public string Value { get; set; }

    public LastName(string value)
    {
        Value = value;
    }

    public static implicit operator LastName(string value) => new (value);
}