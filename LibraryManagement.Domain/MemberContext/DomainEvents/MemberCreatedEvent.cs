using LibraryManagement.Domain.Common;
using MediatR;
namespace LibraryManagement.Domain.MemberContext.DomainEvents
{
    public sealed class MemberCreatedEvent : IDomainEvent
    {
        public Guid MemberId { get; }
        public string Email { get; }
        public DateTime OccurredOn { get; }

        public MemberCreatedEvent(Guid memberId, string email)
        {
            MemberId = memberId;
            Email = email;
            OccurredOn = DateTime.UtcNow;
        }
    }

}
