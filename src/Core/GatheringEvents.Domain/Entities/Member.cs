using System;

using GatheringEvents.Domain.Types;

namespace GatheringEvents.Domain.Entities;

public sealed class Member : Entity
{
    public MemberName MemberName { get; }
    public MemberEmail MemberEmail { get; }
    private Member(
        Guid id,
        MemberName memberName,
        MemberEmail memberEmail) : base(id)
    {
        MemberName = memberName;
        MemberEmail = memberEmail;
    }
    
    public static Either<Member, Error> BuildNew(
        string firstName,
        string lastName,
        string email)
    {
        var buildNameResult = MemberName.BuildNew(firstName, lastName);
        if (buildNameResult.Error is not null) {
            return Either<Member, Error>.Fail(error: buildNameResult.Error);
        }

        var buildEmailResult = MemberEmail.BuildNew(email);
        if (buildEmailResult.Error is not null) {
            return Either<Member, Error>.Fail(error: buildEmailResult.Error);
        }

        var member = new Member(
            id: Guid.NewGuid(),
            memberName: buildNameResult.Value!,
            memberEmail: buildEmailResult.Value!);

        return Either<Member, Error>.Ok(member);
    }
}