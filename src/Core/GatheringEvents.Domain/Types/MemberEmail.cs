using System;

namespace GatheringEvents.Domain.Types;

public record MemberEmail
{
    public string Value { get; }

    private MemberEmail(string email)
    {
        Value = email;
    }

    public static Either<MemberEmail, Error> BuildNew(string email)
    {
        if (email is null)
        {
            return Either<MemberEmail, Error>.Fail(
                        Error.BuildNewArgumentNullException(
                            $"{nameof(MemberEmail)}.{nameof(BuildNew)}", nameof(email)),
                        isUnhandledError: false);
        }
        
        var memberEmail =  new MemberEmail(email);
        
        return Either<MemberEmail, Error>.Ok(memberEmail);
    }
}