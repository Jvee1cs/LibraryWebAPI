namespace LibraryManagement.Infrastructure.Data.Entities
{
    public class MemberEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } 
        public int MaxBooksAllowed { get; set; }
        public int BorrowedBooksCount { get; set; }
        public string PasswordHash { get; set; }
        public List<BookEntity> BorrowedBooks { get; set; } = new();
    }
}
