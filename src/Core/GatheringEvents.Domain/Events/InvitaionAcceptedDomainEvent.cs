using System;

namespace GatheringEvents.Domain.Events;

public sealed record InvitationAcceptedDomainEvent(Guid InvitationId, Guid GatheringId) : IDomainEvent;
