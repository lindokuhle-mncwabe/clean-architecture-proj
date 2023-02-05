using System;

namespace GatheringEvents.Domain.Types;

public record InvitationValidBeforeInHours
{
    public int Value { get; set; }

    public InvitationValidBeforeInHours(int value)
    {
        Value = value;
    }

    public static implicit operator InvitationValidBeforeInHours(int value) => new (value);
    public override string ToString() => Value;
