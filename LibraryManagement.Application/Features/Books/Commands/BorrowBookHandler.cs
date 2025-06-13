using FluentResults;
using LibraryManagement.Application.Errors;
using LibraryManagement.Domain.BookContext.Repositories;
using MediatR;
using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Domain.MemberContext.Repositories;
using LibraryManagement.Domain.BookContext.Exceptions;
using LibraryManagement.Domain.MemberContext.Entities;
using Microsoft.Extensions.DependencyInjection;
namespace LibraryManagement.Application.Features.Books.Commands
{
    public class BorrowBookHandler(IBookRepository bookRepo, IMemberRepository memberRepository) : IRequestHandler<BorrowBookCommand, Result>
    {
        public async Task<Result> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
           
            try
            {

                Member? member = await memberRepository.GetByIdAsync(request.UserId);
                if (member is null)
                    return Result.Fail(new MemberNotFoundError(request.UserId));
                var book = await bookRepo.GetByIdAsync(request.BookId);
                if (book == null)
                    return Result.Fail(new EntityNotFoundError(request.BookId));

                if (book.IsBorrowed)
                    return Result.Fail(new BookAlreadyBorrowedError(request.BookId));

                book.Borrow(member , DateTime.UtcNow.AddDays(0));
                await bookRepo.UpdateAsync(book);
                return Result.Ok();
            }
            catch(BookAlreadyBorrowedException ex) 
            {
                return Result.Fail(new BookAlreadyBorrowedError(Guid.NewGuid()));
            }
            catch(BorrowingLimitExceededException ex)
            {
                return Result.Fail(new BorrowingLimitReachedError(Guid.NewGuid()));
            }

            
        }
    }
}