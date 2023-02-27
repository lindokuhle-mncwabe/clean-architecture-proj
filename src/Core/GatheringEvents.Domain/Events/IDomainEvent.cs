using MediatR;

namespace GatheringEvents.Domain.Events;

public interface IDomainEvent : INotification {}

// MemberCreatedDomainEvent
// GatheringScheduledDomainEvent
// InvitationAcceptedDomainEvent