using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositories;

public interface IGatheringRepository
{
    void Add(Gathering gathering);
}