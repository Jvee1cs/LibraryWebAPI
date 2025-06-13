// Commands/Books/BorrowBookCommand.cs
using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public record BorrowBookCommand(Guid UserId, Guid BookId) : IRequest<Result>;
   
}
