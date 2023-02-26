using System;
using System.Collections.Generic;

using GatheringEvents.Domain.Types;

namespace GatheringEvents.Domain.Entities;

public sealed class Gathering : Entity
{
    #region ~Construtors
    private Gathering(
        Guid id,
        Member owner,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string location,
        int? maximumNumberOfAttendees,
        int? invitationValidBeforeInHours)
        : base(id)
    {
        Owner = owner;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
        MaximumNumberOfAttendees = maximumNumberOfAttendees;
        InvitationValidBeforeInHours = invitationValidBeforeInHours;
    }
    #endregion
   
    #region ~Fields
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();
    #endregion

    #region ~Props
    public Member Owner { get; private set; }
    public GatheringType Type { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime ScheduledAtUtc { get; private set; }
    public string Location { get; private set; } = "TBC";
    public int? MaximumNumberOfAttendees { get; private set; }
    public int? InvitationValidBeforeInHours { get; private set; }
    public DateTime? InvitationExpireAtUtc { get; private set; }
    public int NumberOfAttendees { get; set; }
    public IReadOnlyCollection<Attendee> Attendees  => _attendees.AsReadOnly();
    public IReadOnlyCollection<Invitation> Invitations => _invitations.AsReadOnly();    
    #endregion

    #region ~Methods
    public static Either<Gathering, Error> BuildNew(
        Guid id,
        Member owner,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string location,
        int? maximumNumberOfAttendees,
        int? invitationValidBeforeInHours)
    {
        var gathering = new Gathering(
                Guid.NewGuid(),
                owner,
                type,
                scheduledAtUtc,
                name,
                location,
                maximumNumberOfAttendees,
                invitationValidBeforeInHours);
        switch (gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
                {
                    return Either<Gathering, Error>.Fail(
                        Error.BuildNewArgumentNullException(
                            $"{nameof(Gathering)}.{nameof(BuildNew)}", nameof(maximumNumberOfAttendees)
                        ), 
                        isUnhandledError: false);
                }
                gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitation:
                if (invitationValidBeforeInHours is null)
                {
                    return Either<Gathering, Error>.Fail(
                        Error.BuildNewArgumentNullException( 
                            $"{nameof(Gathering)}.{nameof(BuildNew)}", nameof(invitationValidBeforeInHours)),
                        isUnhandledError: false);
                }
                gathering.InvitationExpireAtUtc =
                    gathering.ScheduledAtUtc.AddHours(-invitationValidBeforeInHours.Value);
                break;
            default:
                return Either<Gathering, Error>.Fail(
                    Error.BuildNewArgumentOutOfRangeException(
                         $"{nameof(Gathering)}.{nameof(BuildNew)}", nameof(GatheringType)),
                    isUnhandledError: false);
        }

        return Either<Gathering, Error>.Ok(gathering);
    }
    
    public Either<Invitation, Error> AddNewInvitation(Member member)
    {
        if (Owner.Id == member.Id) {
            return Either<Invitation, Error>.Fail(
                Error.BuildNewInvalidOperationException(
                    nameof(AddNewInvitation), $"{nameof(Owner)}Id is same as MemberId."),
                isUnhandledError: false);
        }

        if (ScheduledAtUtc < DateTime.UtcNow) {
            return Either<Invitation, Error>.Fail(
                Error.BuildNewInvalidOperationException(
                    nameof(AddNewInvitation), $"{nameof(ScheduledAtUtc)} is in the past."),
                isUnhandledError: false);
        }

        var invitation = new Invitation(
            Guid.NewGuid(),
            member,
            this);

        _invitations.Add(invitation);

        return Either<Invitation, Error>.Ok(invitation);
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
        var fullyBooked = 
            Type == GatheringType.WithFixedNumberOfAttendees
            && NumberOfAttendees == MaximumNumberOfAttendees;

        var expired = 
            Type == GatheringType.WithExpirationForInvitation
            && InvitationExpireAtUtc < DateTime.UtcNow;

        if (fullyBooked || expired) {
            invitation.Expire();

            return null;
        }        

        var attendee = invitation.Accept();           
           
        _attendees.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
    #endregion
}