using LibraryManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.BookContext.Exceptions
{
    public class BookAlreadyBorrowedException : DomainException
    {
        public BookAlreadyBorrowedException() :base("This book is already Borrowed.")
        {
           
        }
    }
}
