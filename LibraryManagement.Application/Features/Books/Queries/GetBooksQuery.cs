using LibraryManagement.Application.Dtos.Response;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries
{
    public record GetBooksQuery() : IRequest<List<BookResponse>>;
}
