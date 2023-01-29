using System;

namespace GatheringEvents.Domain.Entities;

public class Invitation
{
    #region ~Construtor
    internal Invitation(    
        Guid id,
        Member member,
        Gathering gathering)
    {
        Id = id;
        GatheringId = member.Id;
        MemberId = gathering.Id;
        Status = InvitationStatus.Pending;
        CreatedOnUtc = DateTime.Now;
    }
    #endregion

    #region ~ClassProps
    public Guid Id { get; private set; }
    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? UpdatedOnUtc { get; private set; }
    #endregion

    #region ~ClassMethods

    #endregion
}