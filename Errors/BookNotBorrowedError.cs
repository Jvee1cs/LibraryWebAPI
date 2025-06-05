using FluentResults;

namespace Librarykuno.Errors
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