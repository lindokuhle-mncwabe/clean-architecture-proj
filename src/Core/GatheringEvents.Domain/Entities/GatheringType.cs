using System;

namespace GatheringEvents.Domain.Entities;

public enum GatheringType
{
    WithFixedNumberOfAttendees,
    WithExpirationForInvitation,
    InvitationExpireAtUtc
}