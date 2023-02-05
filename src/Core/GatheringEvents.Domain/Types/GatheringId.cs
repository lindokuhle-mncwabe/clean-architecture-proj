using System;

namespace GatheringEvents.Domain.Types;

public record GatheringId
{
    public Guid Value { get; set; }

    public GatheringId(Guid value)
    {
        Value = value;
    }
    
    public static implicit operator GatheringId(Guid value) => new (value);
    public override string ToString() => Value;
}