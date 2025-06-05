using FluentResults;

namespace Librarykuno.Errors
{
    public class BookNotFoundError : Error
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public BookNotFoundError(Guid id) : base($"Book with Id = {id} not found") { }

    }
}
