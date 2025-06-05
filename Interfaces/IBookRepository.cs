using Librarykuno.Models;

namespace Librarykuno.Interfaces
{
    public interface IBookRepository
    {
        //Book Add(Book book);
        Task<Book> AddAsync(Book book);
        //Book? GetById(Guid id);
        //IEnumerable<Book> GetAvailable(string? title = null, string? author = null);
        //IEnumerable<Book> GetOverdue();
        //void Update(Book book);
        
        Task<Book?> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAvailableAsync();
        Task<IEnumerable<Book>> GetOverdueAsync();
        Task UpdateAsync(Book book);
        Task DeleteAsync(Guid id);
    }
}
