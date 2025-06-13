using LibraryManagement.Application.Dtos.Response;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries
{
    public record GetBookByIdQuery(Guid BookId) : IRequest<BookResponse?>;

}
