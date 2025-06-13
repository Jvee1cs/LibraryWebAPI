using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands
{
    public record ReturnBookCommand(Guid UserId, Guid BookId) : IRequest<Result>;

}
