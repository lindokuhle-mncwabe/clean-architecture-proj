using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositores;

public interface IGatheringRepositor
{
    void Add(Gathering gathering);
}