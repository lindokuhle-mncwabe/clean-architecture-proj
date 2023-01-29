using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositories;
public interface IInvitationRepository
{
    void Add(Invitation invitation);
}