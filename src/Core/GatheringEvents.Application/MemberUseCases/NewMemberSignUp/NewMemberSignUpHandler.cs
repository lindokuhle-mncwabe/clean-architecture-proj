using System.Threading;
using System.Threading.Tasks;
using GatheringEvents.Domain.Entities;
using GatheringEvents.Domain.Types;
using MediatR;

namespace GatheringEvents.Application.MemberUseCases.NewMemberSignUp;

public sealed class NewMemberSignUpHandler
{
    // Command
        public sealed record NewMemberSignUpHandlerCommand(
        string FirstName,
        string LastName,
        string Email) : IRequest<Either<Member, Error>>;

    // Handler
    internal sealed class Handler : IRequestHandler<NewMemberSignUpHandlerCommand, Either<Member, Error>>
    {
        public Task<Either<Member, Error>> Handle(NewMemberSignUpHandlerCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

}

