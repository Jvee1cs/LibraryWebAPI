using LibraryManagement.Domain.MemberContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.MemberContext.Repositories
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(Guid id);
        Task UpdateAsync(Member member);
        Task<Member?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(Member member);
        Task<IEnumerable<Member>> GetAllAsync(); 
        Task DeleteAsync(Guid id);  


    }
}
