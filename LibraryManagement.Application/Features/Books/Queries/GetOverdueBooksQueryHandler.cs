
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using MediatR;

public record GetOverdueBooksQuery : IRequest<List<BookResponse>>;


public class GetOverdueBooksHandler : IRequestHandler<GetOverdueBooksQuery, List<BookResponse>>
{
    private readonly IBookQueryService _bookQueryService;

    public GetOverdueBooksHandler(IBookQueryService bookQueryService)
    {
        _bookQueryService = bookQueryService;
    }

    public async Task<List<BookResponse>> Handle(GetOverdueBooksQuery request, CancellationToken cancellationToken)
    {
      
        return await _bookQueryService.GetOverdueBooksAsync(cancellationToken);
    }
}
