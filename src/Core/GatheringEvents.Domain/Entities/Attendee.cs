using System;

namespace GatheringEvents.Domain.Entities;

public class Attendee
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid GatheringId { get; set; }
    public DateTime CreatedOnUtc { get; set; }    
}