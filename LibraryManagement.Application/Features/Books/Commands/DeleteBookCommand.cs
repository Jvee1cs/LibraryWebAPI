using FluentResults;
using LibraryManagement.Application.Dtos.Request;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public record DeleteBookCommand(Guid UserId, Guid BookId) : IRequest<Result>;
    
}
