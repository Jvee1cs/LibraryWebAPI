using FluentResults;

namespace Librarykuno.Errors
{
    public class EntityNotFoundError : Error

    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public EntityNotFoundError(Guid id) : base($"Entity with Id = {id} not found")
        {}
    }

}
