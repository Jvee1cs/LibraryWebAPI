using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Infrastructure.Data.Entities;

namespace LibraryManagement.Infrastructure.Mapping
{
    public static class BookMapper
    {
        public static BookEntity ToEntity(Book book)
        {
            return new BookEntity
            {
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                IsBorrowed = book.IsBorrowed,
                DueDate = book.DueDate,
                CreatedAt = book.CreatedAt,
                BorrowerId = book.BorrowerId
            };
        }

       public static Book ToDomain(BookEntity entity)
        {
           var book = new Book(
           title: entity.Title,
           author: entity.Author,
           isbn: entity.ISBN,
           isBorrowed: entity.IsBorrowed,
           dueDate: entity.DueDate,
           createdAt: entity.CreatedAt
         );

         typeof(Book).GetProperty(nameof(Book.Id))!
         .SetValue(book, entity.Id);
         typeof(Book).GetProperty(nameof(Book.BorrowerId))!
        .SetValue(book, entity.BorrowerId);

            return book;
        }

        public static void ApplyDiff(Book domain, BookEntity entity)
        {
            entity.Title = domain.Title;
            entity.Author = domain.Author;
            entity.ISBN = domain.ISBN;
            entity.IsBorrowed = domain.IsBorrowed;
            entity.DueDate = domain.DueDate;
            entity.CreatedAt = domain.CreatedAt;
            entity.BorrowerId = domain.BorrowerId;
        }



    }
}
