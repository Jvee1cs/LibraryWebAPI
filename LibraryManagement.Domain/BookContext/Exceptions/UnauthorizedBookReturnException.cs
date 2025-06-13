namespace LibraryManagement.Domain.BookContext.Exceptions
{
    public class UnauthorizedBookReturnException : Exception
    {
        public UnauthorizedBookReturnException(Guid bookId, Guid memberId)
            : base($"Book with ID '{bookId}' is not borrowed by member with ID '{memberId}'.")
        { }
    }
}
