using LibraryManagement.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Data.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Author)
                .HasMaxLength(100);

            builder.Property(b => b.ISBN)
                .HasMaxLength(20);

            builder.HasOne(b => b.Borrower).WithMany(b => b.BorrowedBooks).HasForeignKey(b => b.BorrowerId);
    
            //builder.HasOne(b => b.Borrower)
            //    .WithMany(m => m.BorrowedBooks)
            //    .HasForeignKey(b => b.BorrowerId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
