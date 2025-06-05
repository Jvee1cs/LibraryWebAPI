using Librarykuno.Models;
using Librarykuno.Response;

namespace Librarykuno.Interfaces
{
    public interface IMemberRepository
    {
        //Member Add(Member member);
        //Member? GetById(Guid id);
        //void Update(Member member);
        //IEnumerable<Member> GetAll();
        Task AddAsync(Member member);
        Task<Member?> GetByIdAsync(Guid id);
        Task UpdateAsync(Member member);
        Task<IEnumerable<Member>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<Member?> GetByEmailAsync(string email);
    }
}
