using AutoMapper;
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using LibraryManagement.Domain.BookContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookResponse>>
    {
        private readonly IBookQueryService bookQueryService;

        public GetBooksQueryHandler(IBookQueryService bookQueryService )
        {

            this.bookQueryService=bookQueryService;
        }

        public async Task<List<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await bookQueryService.GetBooksAsync();
        }
    }
}
