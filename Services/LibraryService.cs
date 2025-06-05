// Services/LibraryService.cs
using AutoMapper;
using FluentResults;
using Librarykuno.Errors;
using Librarykuno.Interfaces;
using Librarykuno.Models;
using Librarykuno.Request;
using Librarykuno.Response;
using Librarykuno.Services;
using Librarykuno.Successes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

public class LibraryService : ILibraryService
{ 
    private readonly IBookRepository _bookRepo;
    private readonly IMemberRepository _memberRepo;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<Member> _passwordHasher;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bookRepo"></param>
    /// <param name="memberRepo"></param>
    /// <param name="mapper"></param>
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

        if (book.IsBorrowed)
            return Result.Fail(new BookAlreadyBorrowedError(bookId));

        if (member.BorrowedBooksCount >= member.MaxBooksAllowed)
            return Result.Fail(new BorrowingLimitReachedError(memberId));

        book.IsBorrowed = true;
        book.BorrowerId = member.Id;
        book.DueDate = DateTime.UtcNow.AddDays(14);
        member.BorrowedBooksCount++;

        await _bookRepo.UpdateAsync(book);
        await _memberRepo.UpdateAsync(member);

        return Result.Ok().WithSuccess(new BorrowBookSuccess(bookId));
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

        book.IsBorrowed = false;
        book.BorrowerId = null;
        book.DueDate = null;
        member.BorrowedBooksCount--;

        await _bookRepo.UpdateAsync(book);
        await _memberRepo.UpdateAsync(member);

        return Result.Ok().WithSuccess(new BookReturnedSuccess(bookId)); 
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

        await _bookRepo.DeleteAsync(bookId);
        return Result.Ok().WithSuccess("Book deleted successfully");
    }

    public async Task<Result> DeleteMember(Guid memberId)
    {
        var member = await _memberRepo.GetByIdAsync(memberId);
        if(member == null)
            return Result.Fail(new BookNotFoundError(memberId));

        await _memberRepo.DeleteAsync(memberId);
        return Result.Ok().WithSuccess("Member deleted successfully");
    }
    public async Task<Result> UpdateMemberAsync(Guid id, UpdateMemberRequest dto)
    {
        var member = await _memberRepo.GetByIdAsync(id);
        if (member == null)
            return Result.Fail(new EntityNotFoundError(id));

        member.Name = dto.Name;
        member.Email = dto.Email;

        await _memberRepo.UpdateAsync(member);

        return Result.Ok().WithSuccess("Member updated successfully");
    }
    public async Task<Result> UpdateBookAsync(Guid id, UpdateBookRequest dto)
    {
        var book = await _bookRepo.GetByIdAsync(id);
        if (book == null)
            return Result.Fail(new EntityNotFoundError(id));

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.ISBN = dto.ISBN;
        await _bookRepo.UpdateAsync(book);

        return Result.Ok().WithSuccess("Book updated successfully");
    }
   

}
