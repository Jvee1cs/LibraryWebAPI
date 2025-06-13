using LibraryManagement.Domain.Common;

namespace LibraryManagement.Domain.BookContext.Exceptions
{
    public class BorrowingLimitExceededException : DomainException
    {
        public BorrowingLimitExceededException() : base("Member cannot borrow more than 3 books")
        {
            
        }
    }
}
