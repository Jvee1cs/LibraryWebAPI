using Librarykuno.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librarykuno.Data.EntityConfiguration
{

    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
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

            // Initialize the list in model constructor, so no need to configure here
        }
    }

}
