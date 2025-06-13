using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.MemberContext.Entities;
using LibraryManagement.Domain.MemberContext.Repositories;
using LibraryManagement.Infrastructure.Common;
using LibraryManagement.Infrastructure.Data.Entities;
using LibraryManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        //private readonly Dictionary<Guid, Member> _members = new();
        private readonly LibraryDbContext _context;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MemberRepository(LibraryDbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            _context = context;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public async Task AddAsync(Member member)
        {
            var entity = MemberMapper.ToEntity(member);
            _context.Members.Add(entity);
            await _context.SaveChangesAsync();
            await _domainEventDispatcher.DispatchAsync(member.DomainEvents);
            member.ClearDomainEvents();

        }

        public async Task<Member?> GetByIdAsync(Guid id)
        {
            //return await _context.Members
            //    .Include(m => m.BorrowedBooks) 
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var entity = await _context.Members
                .Include(m => m.BorrowedBooks)
                
                .FirstOrDefaultAsync(m => m.Id == id);

            return entity is null ? null : MemberMapper.ToDomain(entity);
        }

        public async Task UpdateAsync(Member member)
        {
            MemberEntity? existingMember = await _context.Members.FindAsync(member.Id);
            if (existingMember is null)
                return;

            MemberMapper.ApplyDiff(member, existingMember);

            await _context.SaveChangesAsync();
            await _domainEventDispatcher.DispatchAsync(member.DomainEvents);
            member.ClearDomainEvents();

        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            var entities = await _context.Members
                 .Include(m => m.BorrowedBooks)
                 .AsNoTracking()
                 .ToListAsync();

            return entities.Select(MemberMapper.ToDomain);
        }

        public async Task DeleteAsync(Guid id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return;

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            
        }
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email == email);
        }

        public async Task<Member?> GetByEmailAsync(string email)
        {
            var entity = await _context.Members
      .Include(m => m.BorrowedBooks)
      .AsNoTracking()
      .FirstOrDefaultAsync(m => m.Email == email);

            return entity is null ? null : MemberMapper.ToDomain(entity);
        }
    }
}