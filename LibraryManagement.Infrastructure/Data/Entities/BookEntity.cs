using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Data.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? BorrowerId { get; set; }
        public MemberEntity Borrower { get; set; }
        public DateTime? DueDate { get; set; }

        
    }
}
