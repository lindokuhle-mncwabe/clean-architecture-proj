using System;

namespace GatheringEvents.Domain.Entities;

public sealed class Attendee
{
    internal Attendee(Invitation invitation)
    {
        GatheringId = invitation.GatheringId;
        MemberId = invitation.MemberId;
        CreatedOnUtc = DateTime.UtcNow;
    }
    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }    
}