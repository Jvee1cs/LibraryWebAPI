using System.Threading.Tasks;
using MediatR;

namespace LibraryManagement.Domain.Common
{
    public class MediatRPublisher : IPublisher
    {
        private readonly IMediator _mediator;

        public MediatRPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync(IDomainEvent domainEvent)
        {
            // MediatR treats domain events as notifications
            await _mediator.Publish(domainEvent);
        }
    }
}
