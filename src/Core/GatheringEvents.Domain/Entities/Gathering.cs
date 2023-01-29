using System;
using System.Collections.Generic;

namespace GatheringEvents.Domain.Entities;

public class Gathering
{
    #region Construtor

    private Gathering(
        Guid id,
        Member owner,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string location,
        int? maximumNumberOfAttendees,
        int? invitationValidBeforeInHours)
    {
        Id = id;
        Owner = owner;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
        MaximumNumberOfAttendees = maximumNumberOfAttendees;
        InvitationValidBeforeInHours = invitationValidBeforeInHours;
    }

    #endregion
   
    #region Class Props

    public Guid Id { get; private set; }
    public Member Owner { get; private set; } = new ();
    public GatheringType Type { get; private set; } = new ();
    public string Name { get; private set; } = string.Empty;
    public DateTime ScheduledAtUtc { get; private set; }
    public string Location { get; private set; } = "TBC";
    public int? MaximumNumberOfAttendees { get; private set; }
    public int? InvitationValidBeforeInHours { get; private set; }
    public DateTime? InvitationExpireAtUtc { get; private set; }
    public int NumberOfAttendees { get; set; }
    public List<Attendee> Attendees { get; set; } = new List<Attendee>();
    public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    
    #endregion

    #region Class Methods

    public static Gathering ScheduleNew(
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
                gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees is null ? 
                        throw new ArgumentNullException(nameof(maximumNumberOfAttendees)) :
                        maximumNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitation:
                gathering.InvitationExpireAtUtc = invitationValidBeforeInHours is null ?
                    throw new ArgumentNullException(nameof(invitationValidBeforeInHours)) :
                    gathering.ScheduledAtUtc.AddHours(-invitationValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }

        return gathering;
    }

    #endregion
}