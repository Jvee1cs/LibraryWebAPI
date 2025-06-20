﻿namespace Librarykuno.Response
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public BorrowerResponse Borrower { get; set; }
    }
}
