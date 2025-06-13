using LibraryManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookContext.DomainEvents
{
    public sealed class BookCreatedEvent : IDomainEvent
    {
        public Guid BookId { get; }
        public string Title { get; }
        public string Author { get; }

        public BookCreatedEvent(Guid bookId, string title, string author)
        {
            BookId = bookId;
            Title = title;
            Author = author;
        }
    }
}
