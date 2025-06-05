using FluentResults;

namespace Librarykuno.Successes
{
    public class BorrowBookSuccess : Success
    {
        /// <summary>
        /// 
        /// </summary>
        public BorrowBookSuccess(Guid id)
            : base("Book borrowed successfully")
        {
        }
    }
}