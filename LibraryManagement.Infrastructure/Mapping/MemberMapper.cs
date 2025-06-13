using LibraryManagement.Domain.MemberContext.Entities;
using LibraryManagement.Infrastructure.Data.Entities;

namespace LibraryManagement.Infrastructure.Mapping
{
    public static class MemberMapper
    {
        public static MemberEntity ToEntity(Member member)
        {
            return new MemberEntity
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                PasswordHash = member.PasswordHash,
                MaxBooksAllowed = member.MaxBooksAllowed,
                BorrowedBooksCount = member.BorrowedBooksCount,
            };
        }

        public static Member ToDomain(MemberEntity entity)
        {
            var member = new Member(
                name: entity.Name,
                email: entity.Email,
                passwordHash: entity.PasswordHash,
                maxBooksAllowed: entity.MaxBooksAllowed
            );

            typeof(Member).GetProperty(nameof(Member.Id))!
                .SetValue(member, entity.Id);
            typeof(Member).GetProperty(nameof(Member.BorrowedBooksCount))!
                .SetValue(member, entity.BorrowedBooksCount);


            return member;
        }

        public static void ApplyDiff(Member domain, MemberEntity entity)
        {
            entity.Name = domain.Name;
            entity.Email = domain.Email;
            entity.MaxBooksAllowed = domain.MaxBooksAllowed;
            entity.BorrowedBooksCount = domain.BorrowedBooksCount;
        }
    }
}
