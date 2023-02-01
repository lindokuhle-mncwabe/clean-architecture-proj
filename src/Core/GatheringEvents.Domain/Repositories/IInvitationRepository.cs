using System;
using System.Threading;
using System.Threading.Tasks;
using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositories;
public interface IInvitationRepository
{
    Task<Invitation> GetByIdAsync(Guid invitationId, CancellationToken cancelToken);
    void Add(Invitation invitation);
}