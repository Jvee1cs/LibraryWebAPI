using FluentResults;

namespace LibraryManagement.Application.Errors
{
    public class EmailAlreadyExistsError : Error
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        public EmailAlreadyExistsError(string email)
            : base($"Email '{email}' already exists.")
        {
        }
    }
}