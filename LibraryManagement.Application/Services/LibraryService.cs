using AutoMapper;
using FluentResults;
using LibraryManagement.Application.Dtos.Request;
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.Errors;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Domain.BookContext.Repositories;
using LibraryManagement.Domain.MemberContext.Repositories;

public class LibraryService : ILibraryService
{
    private readonly IBookRepository _bookRepo;
    private readonly IMemberRepository _memberRepo;
    private readonly IMapper _mapper;

    public LibraryService(IBookRepository bookRepo, IMemberRepository memberRepo, IMapper mapper)
    {
        _bookRepo = bookRepo;
        _memberRepo = memberRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MemberResponse>> GetAllMember()
    {
        var members = await _memberRepo.GetAllAsync();
        return _mapper.Map<IEnumerable<MemberResponse>>(members);
    }

    public async Task<BookResponse> AddNewBook(BookRequest request)
    {
        var newBook = _mapper.Map<Book>(request);
        await _bookRepo.AddAsync(newBook);
        return _mapper.Map<BookResponse>(newBook);
    }

    public async Task<Result> BorrowBook(Guid memberId, Guid bookId)
    {
        var member = await _memberRepo.GetByIdAsync(memberId);
        if (member == null)
            return Result.Fail(new EntityNotFoundError(memberId));

        var book = await _bookRepo.GetByIdAsync(bookId);
        if (book == null)
            return Result.Fail(new EntityNotFoundError(bookId));

        try
        {
            // Use domain behavior
            book.Borrow(member, DateTime.UtcNow.AddDays(14));
            
            await _bookRepo.UpdateAsync(book);

            var response = _mapper.Map<BookResponse>(book);
            return Result.Ok();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result> ReturnBook(Guid memberId, Guid bookId)
    {
        var member = await _memberRepo.GetByIdAsync(memberId);
        if (member == null)
            return Result.Fail(new EntityNotFoundError(memberId));

        var book = await _bookRepo.GetByIdAsync(bookId);
        if (book == null)
            return Result.Fail(new EntityNotFoundError(bookId));

        if (!book.IsBorrowed || book.BorrowerId != memberId)
            return Result.Fail(new UnauthorizedReturnError(bookId, memberId));
        try
        {
            // Use domain behavior
            book.Return(memberId);



            await _bookRepo.UpdateAsync(book);
            await _memberRepo.UpdateAsync(member);

            var response = _mapper.Map<BookResponse>(book);
            return Result.Ok();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<IEnumerable<BookResponse>> SearchAvailableBooks()
    {
        var books = await _bookRepo.GetAvailableAsync();
        return _mapper.Map<IEnumerable<BookResponse>>(books);
    }

    public async Task<MemberResponse?> GetMemberById(Guid id)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null) return null;

        return _mapper.Map<MemberResponse>(member);
    }

    public async Task<IEnumerable<BookResponse>> GetOverdueBooks()
    {
        var overdueBooks = await _bookRepo.GetOverdueAsync();
        return _mapper.Map<IEnumerable<BookResponse>>(overdueBooks);
    }

    public async Task<BookResponse?> GetBookById(Guid id)
    {
        var book = await _bookRepo.GetByIdAsync(id);
        if (book == null) return null;

        return _mapper.Map<BookResponse>(book);
    }

    public async Task<Result> DeleteBook(Guid bookId)
    {
        var book = await _bookRepo.GetByIdAsync(bookId);
        if (book == null)
            return Result.Fail(new BookNotFoundError(bookId));

        await _bookRepo.DeleteAsync(book);
        return Result.Ok().WithSuccess("Book deleted successfully");
    }

    public async Task<Result> DeleteMember(Guid memberId)
    {
        var member = await _memberRepo.GetByIdAsync(memberId);
        if (member == null)
            return Result.Fail(new BookNotFoundError(memberId)); // Consider using EntityNotFoundError

        await _memberRepo.DeleteAsync(memberId);
        return Result.Ok().WithSuccess("Member deleted successfully");
    }

    public async Task<Result> UpdateMemberAsync(Guid id, UpdateMemberRequest dto)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null)
            return Result.Fail(new EntityNotFoundError(id));

        _mapper.Map(dto, member);

        await _memberRepo.UpdateAsync(member);
        return Result.Ok().WithSuccess("Member updated successfully");
    }

    public async Task<Result> UpdateBookAsync(Guid id, UpdateBookRequest dto)
    {
        var book = await _bookRepo.GetByIdAsync(id);
        if (book == null)
            return Result.Fail(new EntityNotFoundError(id));

        _mapper.Map(dto, book);

        await _bookRepo.UpdateAsync(book);
        return Result.Ok().WithSuccess("Book updated successfully");
    }
}
