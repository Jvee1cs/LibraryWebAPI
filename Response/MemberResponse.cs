namespace Librarykuno.Response
{
    public class MemberResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int MaxBooksAllowed { get; set; } = 3;
        public int BorrowedBooksCount { get; set; } = 0;
        public List<BookResponse> BorrowedBooks { get; set; } = new();

    }
}
