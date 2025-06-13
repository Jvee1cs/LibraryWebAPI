using FluentResults;
using LibraryManagement.Application.Errors;
using LibraryManagement.Domain.BookContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IBookRepository _bookRepo;
        
        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepo = bookRepository;
            
        }
      

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepo.GetByIdAsync(request.BookId);
            if (book == null)
                return Result.Fail(new BookNotFoundError(request.BookId));

            await _bookRepo.DeleteAsync(book);
            return Result.Ok().WithSuccess("Book deleted successfully");
        }
    }
}
