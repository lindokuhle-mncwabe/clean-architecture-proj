using System;
using System.Collections.Generic;

namespace GatheringEvents.Domain.Entities;

public class Gathering
{
    #region Construtor

    public Gathering(
        Guid id,
        Member owner,
        GatheringType type,
        DateTime scheduledAtUtc,
        string name,
        string location)
    {
        Id = id;
        Owner = owner;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
    }

    #endregion
   
    #region Class Props

    public Guid Id { get; set; }
    public Member Owner { get; set; } = new ();
    public GatheringType Type { get; set; } = new ();
    public string Name { get; set; } = string.Empty;
    public DateTime ScheduledAtUtc { get; set; }
    public string Location { get; set; } = "TBC";
    public int? MaximumNumberOfAttendees { get; set; }
    public DateTime? InvitationExpireAtUtc { get; set; }
    public int NumberOfAttendees { get; set; }
    public List<Attendee> Attendees { get; set; } = new List<Attendee>();
    public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    
    #endregion

    #region Class Methods

    public static Gathering Schedule(
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
                location);

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