using FluentResults;
using LibraryManagement.Application.Errors;
using LibraryManagement.Domain.MemberContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands
{
    public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand, Result>
    {
        private readonly IMemberRepository _memberRepo;

        public DeleteMemberHandler(IMemberRepository memberRepository)
        {
            _memberRepo = memberRepository;

        }


        public async Task<Result> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepo.GetByIdAsync(request.Member);
            if (member == null)
                return Result.Fail(new MemberNotFoundError(request.Member));

            await _memberRepo.DeleteAsync(request.Member);
            return Result.Ok().WithSuccess("Member deleted successfully");
        }
    }
}
