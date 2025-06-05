using System.Text.Json.Serialization;

namespace Librarykuno.Models
{
    public class Member
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int MaxBooksAllowed { get; set; } = 3;
        public int BorrowedBooksCount { get; set; } = 0;

        public ICollection<Book> BorrowedBooks { get; set; } = new List<Book>();
        public string PasswordHash { get; set; }
    }
}
