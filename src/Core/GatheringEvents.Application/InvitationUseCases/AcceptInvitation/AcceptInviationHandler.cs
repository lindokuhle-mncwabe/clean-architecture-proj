namespace GatheringEvents.Application.InvitationUseCases.AcceptInvitation;

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using GatheringEvents.Domain.Repositories;
using GatheringEvents.Application.Abstractions;
using GatheringEvents.Domain.Entities;
using GatheringEvents.Domain.Types;

public sealed class AcceptInvitationHandler
{
    // Command
    public sealed record AcceptInvitationCommand(
        Guid InvitationId): IRequest<Result<Attendee, Error>>;

    // Handler
    internal sealed class Handler : IRequestHandler<AcceptInvitationCommand, Result<Attendee, Error>>
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

        public async Task<Result<Attendee, Error>> Handle(
            AcceptInvitationCommand request, 
            CancellationToken cancellationToken)
        {
            var invitation = 
                await _invitationRepository.GetByIdAsync(
                    request.InvitationId, 
                    cancellationToken);

            if (invitation is null) { 
                return Result<Attendee, Error>.Fail(
                    new Error($"{nameof(ArgumentNullException)} (Parameter `{nameof(invitation)}`)"),
                    false);
            }

            if (invitation.Status != InvitationStatus.Pending){
                return Result<Attendee, Error>.Fail(
                    new Error($"{nameof(InvalidOperationException)} (Parameter `{nameof(InvitationStatus)}:{invitation.Status}`)"),
                    false);
            }

            var member = 
                await _memberRepository.GetByIdAsync(
                    invitation.MemberId, 
                    cancellationToken);

            var gathering = 
                await _gatheringRepository.GetByIdWithOwnerAsync(
                    invitation.GatheringId,
                    cancellationToken);

            if (member is null) { 
                return Result<Attendee, Error>.Fail(
                    new Error($"{nameof(ArgumentNullException)} (Parameter `{nameof(member)}`)"),
                    false); 
            }
            
            if (gathering is null) {
                return Result<Attendee, Error>.Fail(
                    new Error($"{nameof(ArgumentNullException)} (Parameter `{nameof(gathering)}`)"),
                    false);
            }
            
            var attendee = gathering.AcceptInvitation(invitation);
            
            if (attendee is null) {
                return Result<Attendee, Error>.Fail(
                    new Error($"{nameof(ArgumentNullException)} (Parameter `{nameof(attendee)}`)"),
                    false);
            }

            _attendeeRepository.Add(attendee); 
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (invitation.Status == InvitationStatus.Accepted) {
                await _emailService.SendInvitationAcceptedEmail(gathering, cancellationToken); 
            } 

            return Result<Attendee, Error>.Ok(attendee);
        }
    }
}