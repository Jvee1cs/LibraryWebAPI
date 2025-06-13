using LibraryManagement.Domain.BookContext.Entities;

namespace LibraryManagement.Domain.BookContext.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
    Task<bool> ExistsAsync(Guid id);
    Task<IEnumerable<Book>> GetAvailableAsync();
    Task<IEnumerable<Book>> GetOverdueAsync();
}
