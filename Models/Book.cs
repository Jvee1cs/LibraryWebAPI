using System.ComponentModel.DataAnnotations;

namespace Librarykuno.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public bool IsBorrowed { get; set; } = false;
        public Guid? BorrowerId { get; set; }
        public Member? Borrower { get; set; }
        public DateTime? DueDate { get; set; }
  
    }
}