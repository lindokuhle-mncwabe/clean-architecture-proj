using System;

namespace GatheringEvents.Domain.Types;

public sealed record MemberEmail
{
    public string Value { get; }

    private MemberEmail(string email)
    {
        Value = email;
    }

    public static Either<MemberEmail, Error> BuildNew(string email)
    {
        if (email is null) {
            return 
                Either<MemberEmail, Error>.Fail(
                    Error.BuildNewArgumentNullException( 
                        operation: $"{nameof(MemberEmail)}.{nameof(BuildNew)}", 
                        parameterName: nameof(email)));
        }
        
        var memberEmail =  new MemberEmail(email);
        
        return Either<MemberEmail, Error>.Ok(memberEmail);
    }
}