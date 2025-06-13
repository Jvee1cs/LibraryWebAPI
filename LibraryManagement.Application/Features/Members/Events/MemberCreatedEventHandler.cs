
using LibraryManagement.Domain.MemberContext.DomainEvents;
using LibraryManagement.Domain.MemberContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Events
{
    public class MemberCreatedEventHandler : INotificationHandler<MemberCreatedEvent>
    {
        private readonly IMemberRepository memberRepository;

        public MemberCreatedEventHandler(IMemberRepository memberRepository)
        {
            this.memberRepository=memberRepository;
        }
        public Task Handle(MemberCreatedEvent notification, CancellationToken cancellationToken)
        {

            Console.WriteLine($"🎉 Member created with ID: {notification.MemberId}, Email: {notification.Email}");
            return Task.CompletedTask;
        }
    }
}
