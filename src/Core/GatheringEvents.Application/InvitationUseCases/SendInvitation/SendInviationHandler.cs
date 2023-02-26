using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using GatheringEvents.Application.Abstractions;
using GatheringEvents.Domain.Entities;
using GatheringEvents.Domain.Repositories;
using GatheringEvents.Domain.Types;

namespace GatheringEvents.Application.InvitationUseCases.SendInvitation;

public sealed class SendInvitationHandler
{
    // Command
    public sealed record SendInvitationCommand(
        Guid MemberId, 
        Guid GatheringId) : IRequest<Either<Invitation, Error>>;

    // Handler
    internal sealed class Handler : IRequestHandler<SendInvitationCommand, Either<Invitation, Error>>
    {

        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public Handler(
            IMemberRepository memberRepository,
            IGatheringRepository gatheringRepository,
            IInvitationRepository invitationRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _invitationRepository = invitationRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Either<Invitation, Error>> Handle(SendInvitationCommand request, CancellationToken cancelToken)
        {
            var member = 
                await _memberRepository.GetByIdAsync(
                    request.MemberId,
                    cancelToken);

            var gathering = 
                await _gatheringRepository.GetByIdAsync(
                    request.GatheringId,
                    cancelToken);

            if (member is null) { 
                return Either<Invitation, Error>.Fail(
                    Error.BuildNewArgumentNullException(
                        operation: nameof(SendInvitationCommand), 
                        parameterName: nameof(member)));
            }
            
            if (gathering is null) {
                return  Either<Invitation, Error>.Fail(
                    Error.BuildNewArgumentNullException(
                        operation: nameof(SendInvitationCommand), 
                        parameterName: nameof(gathering)));
            }

            var result = gathering.AddNewInvitation(member);    

            if (result.Value is null) return result;

            _invitationRepository.Add(result.Value);

            await _unitOfWork.SaveChangesAsync(cancelToken);

            await _emailService.SendInvitationEmail(member, gathering, cancelToken);

            return Either<Invitation, Error>.Ok(result.Value);
        }
    }
}