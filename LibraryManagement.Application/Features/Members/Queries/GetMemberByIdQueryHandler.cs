using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries
{
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberResponse?>
    {
        private readonly IMemberQueryService memberQueryService;

        public GetMemberByIdQueryHandler(IMemberQueryService memberQueryService)
        {
            this.memberQueryService=memberQueryService;
        }

        public async Task<MemberResponse?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            return await memberQueryService.GetMemberByIdAsync(request.MemberId);
        }
    }
}
