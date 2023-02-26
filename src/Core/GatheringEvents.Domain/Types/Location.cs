using System;

namespace GatheringEvents.Domain.Types;

public sealed record Location
{
    public string Value { get; set; }

    public Location(string value)
    {
        Value = value;
    }

    public static implicit operator Location(string value) => new (value);
}