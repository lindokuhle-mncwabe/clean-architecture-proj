using System;

namespace GatheringEvents.Domain.Types;

public record MemberName
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
        if (firstName is null)
        {
            return Either<MemberName, Error>.Fail(
                        error: new Error($"{nameof(ArgumentNullException)} (Parameter `{nameof(firstName)}`)"),
                        isUnhandledError: false);
        }
        if (lastName is null)
        {
            return Either<MemberName, Error>.Fail(
                        error: new Error($"{nameof(ArgumentNullException)} (Parameter `{nameof(lastName)}`)"),
                        isUnhandledError: false);
        }

        var memberName =  new MemberName(
                firstName: firstName,
                lastName: lastName);
        
        return Either<MemberName, Error>.Ok(memberName);
    }
  
}