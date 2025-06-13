using FluentResults;

namespace LibraryManagement.Application.Errors
{
    public class UnauthorizedReturnError : Error
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="UserId"></param>
        public UnauthorizedReturnError(Guid BookId, Guid UserId)
            : base($"Book with ID '{BookId}' is not borrowed by member with ID '{UserId}'.")
        {
        }
    }
}