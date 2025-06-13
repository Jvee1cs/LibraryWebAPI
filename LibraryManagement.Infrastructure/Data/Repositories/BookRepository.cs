using LibraryManagement.Domain.BookContext.Repositories;
using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Data.Entities;
using LibraryManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using MediatR;
using LibraryManagement.Domain.Common;

namespace LibraryManagement.Infrastructure.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        //private readonly IPublisher publisher;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        //IPublisher publisher
        public BookRepository(LibraryDbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            _context = context;
            //this.publisher=publisher;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<Book> AddAsync(Book book)
        {
            var entity = BookMapper.ToEntity(book);
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return book;
           
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return entity is null ? null : BookMapper.ToDomain(entity);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var entities = await _context.Books.AsNoTracking().ToListAsync();
            return entities.Select(BookMapper.ToDomain);
        }

        public async Task<Book?> GetAllAuthor(Guid id)
        {
            // This is misleading as a method name.
            // Consider renaming to GetByIdWithAuthorAsync if including author later.
            var entity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return entity is null ? null : BookMapper.ToDomain(entity);
        }

        public async Task<IEnumerable<Book>> GetAvailableAsync()
        {
            var entities = await _context.Books
                .AsNoTracking()
                .Where(b => !b.IsBorrowed)
                .ToListAsync();

            return entities.Select(BookMapper.ToDomain);
        }

        public async Task<IEnumerable<Book>> GetOverdueAsync()
        {
            var entities = await _context.Books
                .AsNoTracking()
                .Where(b => b.IsBorrowed && b.DueDate < DateTime.UtcNow)
                .ToListAsync();

            return entities.Select(BookMapper.ToDomain);
        }

        public async Task UpdateAsync(Book book)
        {
            var existingEntity = await _context.Books.FindAsync(book.Id);
            if (existingEntity is null) return;
            

            BookMapper.ApplyDiff(book, existingEntity);
            await _context.SaveChangesAsync();
            await _domainEventDispatcher.DispatchAsync(book.DomainEvents); 

            //foreach (IDomainEvent domainevent in book.DomainEvents)
            //{
            //    await publisher.Publish(domainevent);
            //}

            book.ClearDomainEvents();


        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book is null) return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            var entity = await _context.Books.FindAsync(book.Id);
            if (entity is null) return;

            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }

        Task IBookRepository.AddAsync(Book book)
        {
            return AddAsync(book);
        }
    }
}
