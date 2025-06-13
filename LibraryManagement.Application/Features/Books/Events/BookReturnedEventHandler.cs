using LibraryManagement.Domain.BookContext.DomainEvents;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Events
{
    public sealed class BookReturnedEventHandler : INotificationHandler<BookReturnedEvent>
    {
        public Task Handle(BookReturnedEvent notification, CancellationToken ct)
        {
          
            Console.WriteLine(
                $" Book {notification.BookId} returned by member {notification.MemberId} at {notification.OccurredOn:g}");

            return Task.CompletedTask;
        }
    }
}
