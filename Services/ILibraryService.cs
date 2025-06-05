using FluentResults;
using Librarykuno.Models;
using Librarykuno.Request;
using Librarykuno.Response;
using Microsoft.AspNetCore.Identity.Data;

namespace Librarykuno.Services
{
    public interface ILibraryService
    {
        Task<BookResponse> AddNewBook(BookRequest book);
        Task<MemberResponse?> GetMemberById(Guid id);
        Task<BookResponse?> GetBookById(Guid id);
        Task<IEnumerable<BookResponse>> SearchAvailableBooks();
        Task<IEnumerable<BookResponse>> GetOverdueBooks();
        Task<Result> BorrowBook(Guid memberId, Guid bookId);
        Task<Result> ReturnBook(Guid memberId, Guid bookId);
        Task<IEnumerable<MemberResponse>> GetAllMember();
        Task<Result>DeleteBook(Guid bookId);
        Task<Result>DeleteMember(Guid memberId);
        Task<Result> UpdateMemberAsync(Guid id, UpdateMemberRequest dto);
        Task<Result> UpdateBookAsync(Guid id, UpdateBookRequest dto);

    }
}

