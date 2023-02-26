using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using GatheringEvents.Domain.Entities;
using GatheringEvents.Domain.Repositories;
using GatheringEvents.Domain.Types;

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
        int? InvitationValidBeforeInHours) : IRequest<Either<Gathering, Error>>;

    // Handler
    internal sealed class Handler : IRequestHandler<ScheduleGatheringCommand, Either<Gathering, Error>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(
            IMemberRepository memberRepository,
            IGatheringRepository gatheringRepository,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Either<Gathering, Error>> Handle(ScheduleGatheringCommand request, CancellationToken cancelToken)
        {
            var member = 
                await _memberRepository.GetByIdAsync(
                    request.MemberId, 
                    cancelToken); 

            if (member is null) {
                return Either<Gathering, Error>.Fail(
                    Error.BuildNewArgumentNullException(
                        operation: nameof(ScheduleGatheringCommand), 
                        parameterName: nameof(member)));
            }

            var result = Gathering.BuildNew(
                Guid.NewGuid(),
                member,
                request.Type,
                request.ScheduledAtUtc,
                request.Name,
                request.Location ?? "TBC",
                request.MaximumNumberOfAttendees,
                request.InvitationValidBeforeInHours);

            if (result.Value is null) return result;
            
            _gatheringRepository.Add(result.Value);
            
            await _unitOfWork.SaveChangesAsync(cancelToken);

            return Either<Gathering, Error>.Ok(result.Value);
        }
    }
}
