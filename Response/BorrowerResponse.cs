namespace Librarykuno.Response
{
    public class BorrowerResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int MaxBooksAllowed { get; set; }
        public int BorrowedBooksCount { get; set; }
    }
}
