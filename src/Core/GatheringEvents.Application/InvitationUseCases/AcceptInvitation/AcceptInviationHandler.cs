namespace GatheringEvents.Application.InvitationUseCases.AcceptInvitation;

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using GatheringEvents.Domain.Repositories;
using GatheringEvents.Application.Abstractions;
using GatheringEvents.Domain.Types;

public sealed class AcceptInvitationHandler
{
    // Command
    public sealed record AcceptInvitationCommand(
        Guid InvitationId): IRequest;

    // Handler
    internal sealed class Handler : IRequestHandler<AcceptInvitationCommand>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public Handler(
            IAttendeeRepository attendeeRepository,
            IMemberRepository memberRepository,
            IGatheringRepository gatheringRepository,
            IInvitationRepository invitationRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _attendeeRepository = attendeeRepository;
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _invitationRepository = invitationRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(
            AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = 
                await _invitationRepository.GetByIdAsync(
                    request.InvitationId, 
                    cancellationToken);

            if (invitation is null || invitation.Status != InvitationStatus.Pending) { 
                return Unit.Value; 
            }

            var member = 
                await _memberRepository.GetByIdAsync(
                    invitation.MemberId, 
                    cancellationToken);

            var gathering = 
                await _gatheringRepository.GetByIdWithOwnerAsync(
                    invitation.GatheringId,
                    cancellationToken);

            if (member is null || gathering is null) { 
                return Unit.Value; 
            }
            
            var attendee = gathering.AcceptInvitation(invitation);
            
            if (attendee is not null) { 
                _attendeeRepository.Add(attendee); 
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (invitation.Status == InvitationStatus.Accepted) {
                await _emailService.SendInvitationAcceptedEmail(gathering, cancellationToken); 
            } 

            return Unit.Value;
        }
    }
}