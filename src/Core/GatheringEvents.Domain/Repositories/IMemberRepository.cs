using System;
using System.Threading;
using System.Threading.Tasks;
using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid memberId, CancellationToken cancelToken);
    void Add(Member member);
}