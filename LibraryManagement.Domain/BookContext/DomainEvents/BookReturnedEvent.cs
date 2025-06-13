using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.BookContext.DomainEvents
{
    public sealed class BookReturnedEvent : IDomainEvent
    {
        public Guid BookId { get; }
        public Guid MemberId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public BookReturnedEvent(Guid bookId, Guid memberId)
        {
            BookId   = bookId;
            MemberId = memberId;
        }
    }
}
