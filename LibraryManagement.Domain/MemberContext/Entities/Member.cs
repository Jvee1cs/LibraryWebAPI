using LibraryManagement.Domain.BookContext.Entities;
using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.MemberContext.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LibraryManagement.Domain.MemberContext.Entities
{
    public class Member : Entity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public int MaxBooksAllowed { get; private set; } = 3;
        public int BorrowedBooksCount { get; private set; } = 0;
        public string PasswordHash { get; private set; } = null!;



        // Constructor
        public Member(string name, string email, string passwordHash, int maxBooksAllowed = 3)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            MaxBooksAllowed = maxBooksAllowed;
            RaiseDomainEvent(new MemberCreatedEvent(Id, Email));

        }

        // Domain Behavior (optional)
        public bool CanBorrow() => BorrowedBooksCount < MaxBooksAllowed;

        public void DecreaseBorrowCount()
        {
            if (BorrowedBooksCount > 0)
                BorrowedBooksCount--;
        }
        public void IncreaseBorrowCount()
        {
            BorrowedBooksCount += BorrowedBooksCount + 1;
        }
        public void UpdateHash(string hash)
        {
            PasswordHash = hash;
        }
    }
}
