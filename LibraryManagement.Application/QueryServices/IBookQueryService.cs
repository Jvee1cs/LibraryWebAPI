using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Domain.BookContext.Entities;

namespace LibraryManagement.Application.QueryServices
{
    public interface IBookQueryService
    {
        Task<BookResponse?> GetBookByIdAsync(Guid id);

        Task<List<BookResponse>> GetBooksAsync();
        Task<List<BookResponse>> GetOverdueBooksAsync(CancellationToken ct = default);
    }
}
