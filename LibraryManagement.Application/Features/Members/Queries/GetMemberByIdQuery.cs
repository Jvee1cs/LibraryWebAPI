using LibraryManagement.Application.Dtos.Response;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries
{
    
        public record GetMemberByIdQuery(Guid MemberId) : IRequest<MemberResponse?>;
    
}
