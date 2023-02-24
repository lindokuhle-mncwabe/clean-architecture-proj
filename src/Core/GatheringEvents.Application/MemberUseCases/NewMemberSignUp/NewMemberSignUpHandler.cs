using System.Threading;
using System.Threading.Tasks;
using GatheringEvents.Domain.Entities;
using GatheringEvents.Domain.Types;
using GatheringEvents.Domain.Repositories;
using GatheringEvents.Application.Abstractions;
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
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public Handler(
            IMemberRepository memberRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        public async Task<Either<Member, Error>> Handle(NewMemberSignUpHandlerCommand request, CancellationToken cancellationToken)
        {
            var member = Member.BuildNew(
                request.FirstName,
                request.LastName,
                request.Email);

            if (member.Error is not null)
            {
                return Either<Member, Error>.Fail(
                    error: member.Error,
                    isUnhandledError: member.IsUnhandledError);
            }

            _memberRepository.Add(member.Value!);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Either<Member, Error>.Ok(member.Value!);
        }
    }
}

