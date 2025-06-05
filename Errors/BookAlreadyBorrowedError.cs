using FluentResults;

namespace Librarykuno.Errors
{
    public class BookAlreadyBorrowedError : Error
    {
        /// <summary>
        /// 
        /// </summary>
        public BookAlreadyBorrowedError(Guid id) : base("Book is already borrowed") { }

    }
}
