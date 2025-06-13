using LibraryManagement.Application.Dtos.Response;
using MediatR;
using System.Collections.Generic;
namespace LibraryManagement.Application.Features.Books.Queries
{
 
    public class GetOverdueBooksQuery : IRequest<List<BookResponse>>
    {
    }

}
