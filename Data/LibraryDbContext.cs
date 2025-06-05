using Librarykuno.Data.EntityConfiguration;
using Librarykuno.Models;
using Microsoft.EntityFrameworkCore;

namespace Librarykuno.Data
{
    public class LibraryDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Configure one-to-many relationship between Member and Book
            //modelBuilder.Entity<Book>()
            //    .HasOne(b => b.Borrower)        // Book has one Borrower (Member)
            //    .WithMany(m => m.BorrowedBooks) // Member has many BorrowedBooks
            //    .HasForeignKey(b => b.BorrowerId)
            //    .OnDelete(DeleteBehavior.SetNull); // If Member deleted, set BorrowerId to null on Book

            //// Optional: configure properties further if needed, e.g. required fields, max lengths, etc.

            //base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
    }
}