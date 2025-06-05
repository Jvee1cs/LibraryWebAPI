using Librarykuno.Data;
using Librarykuno.Interfaces;
using Librarykuno.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Librarykuno.Response;

namespace Librarykuno.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        //private readonly Dictionary<Guid, Member> _members = new();
        private readonly LibraryDbContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            
        }

        public async Task<Member?> GetByIdAsync(Guid id)
        {
            return await _context.Members
                .Include(m => m.BorrowedBooks) // eager load related books
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
            

        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members
                .Include(m => m.BorrowedBooks)
                .ToListAsync();
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
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
        }



    }
}