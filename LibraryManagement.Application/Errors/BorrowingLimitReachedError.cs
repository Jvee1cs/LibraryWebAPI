using FluentResults;

namespace LibraryManagement.Application.Errors
{
    public class BorrowingLimitReachedError : Error
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberId"></param>
        public BorrowingLimitReachedError(Guid memberId) : base($"Member with Id = {memberId} has reached the borrowing limit") { }
    }
}