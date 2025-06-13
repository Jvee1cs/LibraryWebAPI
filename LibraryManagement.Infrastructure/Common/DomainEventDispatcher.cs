using LibraryManagement.Domain.Common;
using LibraryManagement.Infrastructure.Data;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Common
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly LibraryDbContext _db;

        public DomainEventDispatcher(IMediator mediator, LibraryDbContext db)
        {
            _mediator = mediator;
            _db = db;
        }

        /// <summary>
        /// Variant A — Publish a given list of domain events.
        /// </summary>
        public async Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken ct = default)
        {
            foreach (var domainEvent in events)
                await _mediator.Publish(domainEvent, ct);
        }

        /// <summary>
        /// Variant B — Automatically discover domain events from tracked entities.
        /// </summary>
        public async Task DispatchAsync(CancellationToken ct = default)
        {
            var entities = _db.ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            var allEvents = entities.SelectMany(e => e.DomainEvents).ToList();

            foreach (var domainEvent in allEvents)
                await _mediator.Publish(domainEvent, ct);

            foreach (var entity in entities)
                entity.ClearDomainEvents();
        }
    }
}
