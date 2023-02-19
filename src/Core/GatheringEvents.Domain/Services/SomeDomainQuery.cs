using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Services;

public static class SomeDomainQuery
{
    public delegate bool DoSomeDomainQuery(Gathering gathering, Invitation invitation);
}