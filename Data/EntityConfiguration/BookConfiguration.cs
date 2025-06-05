using Librarykuno.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librarykuno.Data.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Author)
                .HasMaxLength(100);

            builder.Property(b => b.ISBN)
                .HasMaxLength(20);

            builder.HasOne(b => b.Borrower)
                .WithMany(m => m.BorrowedBooks)
                .HasForeignKey(b => b.BorrowerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
