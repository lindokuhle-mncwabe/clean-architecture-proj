using System;

namespace GatheringEvents.Domain.Types;

public record NumberOfAttendees
{
    public int Value { get; set; }

    public NumberOfAttendees(int value)
    {
        Value = value;
    }

    public static implicit operator NumberOfAttendees(int value) => new (value);
    public override string ToString() => Value;
}