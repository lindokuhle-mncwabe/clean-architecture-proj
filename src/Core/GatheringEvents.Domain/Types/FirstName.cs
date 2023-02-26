using System;

namespace GatheringEvents.Domain.Types;

public sealed record FirstName
{
    public string Value { get; set; }

    public FirstName(string value)
    {
        Value = value;
    }

    public static implicit operator FirstName(string value) => new (value);
}