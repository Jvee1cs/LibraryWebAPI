using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Domain.MemberContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.QueryServices
{
    public interface IMemberQueryService
    {
        Task<MemberResponse?> GetMemberByIdAsync(Guid id);

        Task<List<MemberResponse>> GetMemberAsync();
    }
}
