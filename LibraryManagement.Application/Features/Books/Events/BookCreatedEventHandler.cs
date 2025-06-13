using LibraryManagement.Domain.BookContext.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Features.Books.Events
{
    public class BookCreatedEventHandler : INotificationHandler<BookCreatedEvent>
    {
        public Task Handle(BookCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Optional: Do something like logging or sending a welcome message
            Console.WriteLine($"Book created: {notification.Title} by {notification.Author}");

            return Task.CompletedTask;
        }
    }
}
