using FluentResults;

namespace LibraryManagement.Application.Errors
{
    public class BookNotBorrowedError : Error
    {
        /// <summary>
        /// /
        /// </summary>
        public BookNotBorrowedError(Guid id) : base("Book not borrowed by this member") 
        { }
    }
}