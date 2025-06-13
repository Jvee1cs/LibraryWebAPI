using LibraryManagement.Domain.BookContext.DomainEvents;
using LibraryManagement.Domain.MemberContext.Entities;
using LibraryManagement.Domain.MemberContext.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Events
{
    public class BookBorrowedEventHandler : INotificationHandler<BookBorrowedEvent>
    {
        private readonly IMemberRepository memberRepository;

        public BookBorrowedEventHandler(IMemberRepository memberRepository)
        {
            this.memberRepository=memberRepository;
        }
        public async Task Handle(BookBorrowedEvent notification, CancellationToken cancellationToken)
        {

            Member? member = await memberRepository.GetByIdAsync(notification.MemberId);
            if (member is null)
                return;

            member.IncreaseBorrowCount();

            await memberRepository.UpdateAsync(member);
            Console.WriteLine($"Book borrowed by MemberId: {notification.MemberId}");
            
            await Task.CompletedTask;
        }
    }
}
