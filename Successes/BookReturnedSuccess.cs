using FluentResults;

namespace Librarykuno.Successes
{
    public class BookReturnedSuccess : Success
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId"></param>
        public BookReturnedSuccess(Guid bookId)
            : base($"Book with ID {bookId} returned successfully.")
        {
        }
    }

}