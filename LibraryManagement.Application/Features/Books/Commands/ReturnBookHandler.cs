// Application/Features/Books/Commands/ReturnBookHandler.cs
using FluentResults;
using LibraryManagement.Application.Errors;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Domain.BookContext.Exceptions;
using LibraryManagement.Domain.BookContext.Repositories;
using LibraryManagement.Domain.MemberContext.Repositories;
using MediatR;
using System.Threading;

public sealed class ReturnBookHandler(
    IBookRepository bookRepo,
    IMemberRepository memberRepo)
        : IRequestHandler<ReturnBookCommand, Result>
{
    public async Task<Result> Handle(ReturnBookCommand request, CancellationToken ct)
    {
        // 1. Fetch aggregate
        var book = await bookRepo.GetByIdAsync(request.BookId);
        if (book is null)
            return Result.Fail(new EntityNotFoundError(request.BookId));

        try
        {

            // 2. Domain invariants (unauthorized attempts throw)
            book.Return(request.UserId);

            // 3. Update member stats
            var member = await memberRepo.GetByIdAsync(request.UserId);
            member?.DecreaseBorrowCount();

            // 4. Persist changes
            await bookRepo.UpdateAsync(book);
            if (member is not null)
                await memberRepo.UpdateAsync(member);
            Console.WriteLine($"DBG IsBorrowed={book.IsBorrowed}, DB BorrowerId={book.BorrowerId}, Caller={request.UserId}");
            return Result.Ok();
        }
        catch (UnauthorizedReturnAttemptException) // thrown by Book.Return
        {
            return Result.Fail(new UnauthorizedReturnError(request.BookId, request.UserId));
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error(ex.Message));
        }
    }
}
