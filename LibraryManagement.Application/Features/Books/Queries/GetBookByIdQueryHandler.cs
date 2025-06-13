using AutoMapper;
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using LibraryManagement.Domain.BookContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookResponse?>
    {
        private readonly IBookQueryService bookQueryService;

        public GetBookByIdQueryHandler(IBookQueryService bookQueryService)
        {
            this.bookQueryService=bookQueryService;
        }

        public async Task<BookResponse?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await bookQueryService.GetBookByIdAsync(request.BookId);
        }
    }
}
