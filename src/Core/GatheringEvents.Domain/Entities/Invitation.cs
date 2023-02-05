using System;

using GatheringEvents.Domain.Types;

namespace GatheringEvents.Domain.Entities;

public sealed class Invitation : Entity
{
    #region ~Construtor

    internal Invitation(    
        Guid id,
        Member member,
        Gathering gathering) : base(id)
    {
        MemberId = member.Id;
        GatheringId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.Now;
    }
    
    #endregion

    #region ~Props

    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? UpdatedOnUtc { get; private set; }

    #endregion

    #region ~Methods

    internal void Expire()
    {
        Status = InvitationStatus.Expired;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    internal Attendee Accept()
    {
        Status = InvitationStatus.Accepted;
        UpdatedOnUtc = DateTime.UtcNow;

        var attendee = new Attendee(this);

        return attendee;
    }

    #endregion
}