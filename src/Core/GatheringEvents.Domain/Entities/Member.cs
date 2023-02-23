using System;

using GatheringEvents.Domain.Types;

namespace GatheringEvents.Domain.Entities;

public sealed class Member : Entity
{
    public Member(
        Guid id,
        MemberName memberName,
        Email email) : base(id)
    {
        MemberName = memberName;
        Email = email;
    }
    public MemberName MemberName { get; }
    public Email Email { get; }
}