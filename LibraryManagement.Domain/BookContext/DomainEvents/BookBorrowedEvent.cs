using LibraryManagement.Domain.Common;
using MediatR;

namespace LibraryManagement.Domain.BookContext.DomainEvents
{
    public class BookBorrowedEvent : IDomainEvent
    {
        public Guid MemberId { get; set; }
        public DateTime OccurredOn { get; }
        public BookBorrowedEvent(Guid memberId)
        {
            MemberId=memberId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
