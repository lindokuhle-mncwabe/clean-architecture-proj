using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Application.GatheringUseCases.ScheduleGathering;

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
    internal sealed class Handler : IRequestHandler<ScheduleGatheringCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepositor _gatheringRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IMemberRepository memberRepository,
            IGatheringRepository gatheringRepository,
            IUnitOfWork unitOfWork
            )
        {
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ScheduleGatheringCommand request, CancellationToken cancelToken)
        {
            var member = _memberRepository.GetByIdAsync(request.MemberId, cancelToken); 

            if (member is null) return Unit.Value;

            var gathering = new Gathering(
                Guid.NewGuid(),
                member,
                request.Type,
                request.ScheduledAtUtc,
                request.Name,
                request.Location);



            // Todo: return result    
            throw new NotImplementedException();
        }
    }

}
