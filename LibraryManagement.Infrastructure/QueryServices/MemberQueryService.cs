using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using LibraryManagement.Domain.MemberContext.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.QueryServices
{
    public class MemberQueryService : IMemberQueryService
    {
        private readonly LibraryDbContext context;
        private readonly IMapper mapper;

        public MemberQueryService(LibraryDbContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }
        public async Task<MemberResponse?> GetMemberByIdAsync(Guid id)
        {
            return await context.Members.Where(b => b.Id == id).ProjectTo<MemberResponse?>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<List<MemberResponse>> GetMemberAsync()
        {
            return await context.Members.ProjectTo<MemberResponse>(mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
