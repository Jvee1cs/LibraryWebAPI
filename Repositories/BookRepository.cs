using Librarykuno.Data;
using Librarykuno.Interfaces;
using Librarykuno.Models;
using Microsoft.EntityFrameworkCore;

namespace Librarykuno.Repositories
{
    public class BookRepository : IBookRepository
    {
        //private readonly Dictionary<Guid, Book> _books = new();
        private readonly LibraryDbContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books
                .Where(b=> b.Id==id)
                .Include(b=>b.Borrower)
                .FirstOrDefaultAsync();
        }

        public async Task<Book?> GetAllAuthor(Guid id)
        {
            return await _context.Books
                .Where(b => b.Id==id)
                .Include(b => b.Author)
                .FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<Book>> GetAvailableAsync()
        {
            return await _context.Books
                .Where(book => !book.IsBorrowed)
                .ToListAsync(); 
        }

        public async Task<IEnumerable<Book>> GetOverdueAsync()
        {
            
            return await _context.Books
                .Where(b => b.IsBorrowed && b.DueDate < DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            
        }

    }
}
