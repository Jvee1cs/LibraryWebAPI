using AutoMapper;
using FluentResults;
using LibraryManagement.Application.Errors;
using LibraryManagement.Domain.BookContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            Domain.BookContext.Entities.Book? existingBook = await _bookRepository.GetByIdAsync(request.BookId);
            if (existingBook == null)
                return Result.Fail(new BookNotFoundError(request.BookId));

            _mapper.Map(request.Request, existingBook );
            await _bookRepository.UpdateAsync(existingBook);
            return Result.Ok().WithSuccess("Book updated successfully");
        }

    }
}
