using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryManagement.Application.Dtos.Response;
using LibraryManagement.Application.QueryServices;
using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.QueryServices
{
    public class BookQueryService : IBookQueryService
    {
        private readonly LibraryDbContext context;
        private readonly IMapper mapper;

        public BookQueryService(LibraryDbContext context, IMapper mapper)
        {
            this.context=context;
            this.mapper=mapper;
        }
        public async Task<BookResponse?> GetBookByIdAsync(Guid id)
        {
            return await context.Books.Where(b => b.Id == id).ProjectTo<BookResponse?>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<List<BookResponse>> GetBooksAsync()
        {
            return await context.Books.ProjectTo<BookResponse>(mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<List<BookResponse>> GetOverdueBooksAsync(CancellationToken ct = default)
        {
            return await context.Books
                .Where(b => b.IsBorrowed && b.DueDate < DateTime.UtcNow)
                .ProjectTo<BookResponse>(mapper.ConfigurationProvider)
                .ToListAsync(ct);
        }
    }
}
