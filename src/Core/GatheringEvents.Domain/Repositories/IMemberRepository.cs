using System;
using System.Threading;
using System.Threading.Tasks;
using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositores;

public interface IMemberRepository
{
    Task<Member> GetByIdAsync(Guid memberId, CancellationToken cancelToken);
}