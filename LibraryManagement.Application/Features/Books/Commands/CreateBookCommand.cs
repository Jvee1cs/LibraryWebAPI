using LibraryManagement.Application.Dtos.Response;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public record CreateBookCommand(string Title, string? Author, string? ISBN) : IRequest<BookResponse>;
}
