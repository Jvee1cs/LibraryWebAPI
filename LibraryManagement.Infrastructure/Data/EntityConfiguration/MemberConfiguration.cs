using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librarykuno.Data.EntityConfiguration
{

    public class MemberConfiguration : IEntityTypeConfiguration<MemberEntity>
    {
        public void Configure(EntityTypeBuilder<MemberEntity> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.MaxBooksAllowed)
                .HasDefaultValue(3);

            builder.Property(m => m.BorrowedBooksCount)
                .HasDefaultValue(0);
        }
    }

}
