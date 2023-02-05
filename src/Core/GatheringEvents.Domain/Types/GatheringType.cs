using System;

namespace GatheringEvents.Domain.Types;

public enum GatheringType
{
    WithFixedNumberOfAttendees,
    WithExpirationForInvitation,
    InvitationExpireAtUtc
}