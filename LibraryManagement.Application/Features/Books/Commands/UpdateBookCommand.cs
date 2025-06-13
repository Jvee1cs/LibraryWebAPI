using LibraryManagement.Application.Dtos.Request;
using MediatR;
using FluentResults;



namespace LibraryManagement.Application.Features.Books.Commands
{
    public record UpdateBookCommand(Guid BookId, UpdateBookRequest Request) : IRequest<Result>;

}
