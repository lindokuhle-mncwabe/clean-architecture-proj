using System;

namespace GatheringEvents.Domain.Types;

public record MaximumNumberOfAttendees
{
    public int Value { get; set; }

    public MaximumNumberOfAttendees(int value)
    {
        Value = value;
    }

    public static implicit operator MaximumNumberOfAttendees(int value) => new (value);
    public override string ToString() => Value.ToString();
}