using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookContext.Exceptions
{
    public class UnauthorizedReturnAttemptException : Exception
    {
        public UnauthorizedReturnAttemptException(Guid bookId, Guid memberId) : base($"Book with ID '{bookId}' is not borrowed by member with ID '{memberId}'.")
        {
        }
    }

}
