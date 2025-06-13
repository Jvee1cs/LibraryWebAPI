using LibraryManagement.Domain.BookContext.DomainEvents;
using LibraryManagement.Domain.BookContext.Exceptions;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.MemberContext.Entities;
////anemic domain model to rich domain model
//public class Book
//{
//    public Guid Id { get;  set; } 
//    public string Title { get; set; } = null!;
//    public string? Author { get; set; }
//    public string? ISBN { get; set; }
//    public bool IsBorrowed { get; set; } = false;
//    public Guid? BorrowerId { get; set; }
//    public Member? Borrower { get; set; }
//    public DateTime? DueDate { get; set; }

//}

namespace LibraryManagement.Domain.BookContext.Entities
{

    public class Book : Entity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } 
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public bool IsBorrowed { get; private set; }
        public Guid? BorrowerId { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        protected Book() { }
        private Book(Guid id, string title, string author, string isbn, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            CreatedAt = createdAt;
           
        }

        public Book(string title, string author, string isbn, bool isBorrowed, DateTime? dueDate, DateTime createdAt)
        {
            Title=title;
            Author=author;
            ISBN=isbn;
            IsBorrowed=isBorrowed;
            DueDate=dueDate;
            CreatedAt=createdAt;

            RaiseDomainEvent(new BookCreatedEvent(Id, Title, Author));
        }

        public static Book Create(string title, string author, string isbn)
        {
            var book = new Book(Guid.NewGuid(), title, author, isbn, DateTime.UtcNow);
            //book.RaiseDomainEvent(new BookCreatedEvent(Id, Title, Author));
            return book;
        }

        public void Borrow(Member member, DateTime dueDate)
        {
            if (IsBorrowed)
                throw new BookAlreadyBorrowedException();

            if (member.BorrowedBooksCount >= member.MaxBooksAllowed)
                throw new BorrowingLimitExceededException();

            BorrowerId = member.Id;
            IsBorrowed = true;
            DueDate = dueDate;
            RaiseDomainEvent(new BookBorrowedEvent(member.Id));
        }

        public void Return(Guid memberId)
        {
            if (!IsBorrowed || BorrowerId != memberId)
                throw new UnauthorizedBookReturnException(Id, memberId);

            BorrowerId = null;
            IsBorrowed = false;
            DueDate = null;
            RaiseDomainEvent(new BookReturnedEvent(Id, memberId));
        }
    }
}

