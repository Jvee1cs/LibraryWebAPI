using FluentResults;

namespace LibraryManagement.Application.Errors
{
    public class MemberNotFoundError :Error
    {
      /// <summary>
            /// 
            /// </summary>
            /// <param name="id"></param>
            public MemberNotFoundError(Guid id) : base($"Member with Id = {id} not found") { }

        }
    }



