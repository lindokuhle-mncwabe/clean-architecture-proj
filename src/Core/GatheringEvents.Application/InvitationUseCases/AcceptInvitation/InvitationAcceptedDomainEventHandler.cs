using System.Threading;
using System.Threading.Tasks;
using GatheringEvents.Application.Abstractions;
using GatheringEvents.Domain.Events;
using GatheringEvents.Domain.Repositories;
using MediatR;

namespace GatheringEvents.Application.InvitationUseCases.AcceptInvitation;

public sealed class InvitationAcceptedDomainEventHandler
    : INotificationHandler<InvitationAcceptedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IGatheringRepository _gatheringRepository;
    public InvitationAcceptedDomainEventHandler(IEmailService emailService, IGatheringRepository gatheringRepository)
    {
        _emailService = emailService;
        _gatheringRepository = gatheringRepository;
    }
    public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
    {
        var gathering = await _gatheringRepository.GetByIdAsync(
            notification.GatheringId, 
            cancellationToken);

        if (gathering is null) return;

        await _emailService.SendInvitationAcceptedEmail(
            gathering, 
            cancellationToken); 
    }
}