using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Gathering.Domain.Entities;

namespace Gathering.Application.Gatherings.ScheduleGathering;

public sealed class ScheduleGatheringHandler
{
    // Command
    public sealed record ScheduleGatheringCommand(
        Guid MemberId,
        GatheringType Type,
        DateTime ScheduledAtUtc,
        string Name,
        string? Location,
        int? MaximumNumberOfAttendees,
        int? InvitationValidBeforeInHours) : IRequest;

    // Handler
    internal sealed class Handle : IRequestHandler<ScheduleGatheringCommand>
    {
        // private readonly IMemberRepository _memberRepository;
        // private readonly IGatheringRepositor _gatheringRepository;
        // private readonly IUnitOfWork _unitOfWork;

        public ScheduleGatheringHandler(
            // IMemberRepository memberRepository,
            // IGatheringRepository gatheringRepository,
            // IUnitOfWork unitOfWork
            )
        {
            // _memberRepository = memberRepository;
            // _gatheringRepository = gatheringRepository;
            // _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ScheduleGatheringCommand reqest, CancellationToken cancellationToke)
        {
            return Unity.Value;
        }
    }

}
