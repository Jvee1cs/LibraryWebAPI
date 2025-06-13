using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries
{
    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, List<MemberResponse>>
    {
        private readonly IMemberQueryService memberQueryService;

        public GetMembersQueryHandler(IMemberQueryService memberQueryService)
        {

            this.memberQueryService=memberQueryService;
        }

        public async Task<List<MemberResponse>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            return await memberQueryService.GetMemberAsync();
        }
    }
}
