using AutoMapper;
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Domain.BookContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, BookResponse>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IBookQueryService bookQueryService;

        public CreateBookHandler(IBookRepository bookRepository, IMapper mapper, IBookQueryService bookQueryService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            this.bookQueryService = bookQueryService;
        }

        public async Task<BookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(request.Title, request.Author, request.ISBN);
            await _bookRepository.AddAsync(book);
            return _mapper.Map<BookResponse>(book);
        }
    }
}
