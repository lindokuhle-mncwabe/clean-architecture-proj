
using System.Threading;
using System.Threading.Tasks;

using GatheringEvents.Domain.Entities;

namespace GatheringEvents.Application.Abstractions;

public interface IEmailService
{
    Task SendInvitationEmail(Member member, Gathering gathering, CancellationToken cancelToken);
}