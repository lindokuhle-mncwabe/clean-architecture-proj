using System;
using System.Threading;
using System.Threading.Tasks;

using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositories;

public interface IGatheringRepository
{
    Task<Gathering> GetByIdAsync(Guid gatheringId, CancellationToken cancelToken);
    Task<Gathering> GetByIdWithOwnerAsync(Guid gatheringId, CancellationToken cancelToken);
    void Add(Gathering gathering);
}