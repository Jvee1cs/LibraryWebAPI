using FluentResults;

namespace Librarykuno.Errors
{
    public class UnauthorizedReturnError : Error
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="memberId"></param>
        public UnauthorizedReturnError(Guid bookId, Guid memberId)
            : base($"Book with ID '{bookId}' is not borrowed by member with ID '{memberId}'.")
        {
        }
    }
}