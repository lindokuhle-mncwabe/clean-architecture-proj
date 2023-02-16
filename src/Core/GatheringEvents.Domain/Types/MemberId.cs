using System;

namespace GatheringEvents.Domain.Types;

public record MemberId
{
    public Guid Value { get; set; }
    
    public MemberId(Guid value)
    {
        Value = value;
    }

    public static implicit operator MemberId(Guid value) => new (value);
    public override string ToString() => Value.ToString();
}