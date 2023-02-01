using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositories;

public interface IAttendeeRepository
{
    void Add(Attendee attendee);
}