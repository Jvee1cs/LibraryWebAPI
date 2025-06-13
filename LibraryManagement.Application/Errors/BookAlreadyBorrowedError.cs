using FluentResults;

namespace LibraryManagement.Application.Errors
{
    public class BookAlreadyBorrowedError : Error
    {
        /// <summary>
        /// 
        /// </summary>
        public BookAlreadyBorrowedError(Guid id) : base("Book is already borrowed") { }

    }
}
