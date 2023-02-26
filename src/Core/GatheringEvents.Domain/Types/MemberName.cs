using System;

namespace GatheringEvents.Domain.Types;

public sealed record MemberName
{
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }

    private MemberName(FirstName firstName, LastName lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Either<MemberName, Error> BuildNew(
        string firstName,
        string lastName)
    {
        if (firstName is null) {
            return 
                Either<MemberName, Error>.Fail(
                    Error.BuildNewArgumentNullException(
                        operation: $"{nameof(MemberName)}.{nameof(BuildNew)}", 
                        parameterName: nameof(firstName)));
        }

        if (lastName is null) {
            return 
                Either<MemberName, Error>.Fail(
                    Error.BuildNewArgumentNullException( 
                        operation: $"{nameof(MemberName)}.{BuildNew}", 
                        parameterName: nameof(lastName)));
        }

        var memberName =  new MemberName(
                firstName: firstName,
                lastName: lastName);
        
        return Either<MemberName, Error>.Ok(memberName);
    }
  
}