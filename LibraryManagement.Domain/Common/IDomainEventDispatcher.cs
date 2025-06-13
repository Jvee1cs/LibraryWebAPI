using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common
{
    /// <summary>
    /// Publishes domain events raised by your aggregates.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Variant A – Publish the supplied events list.
        /// </summary>
        Task DispatchAsync(
            IEnumerable<IDomainEvent> domainEvents,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Variant B – Publish events discovered internally
        /// (e.g., from an EF Core <c>DbContext</c>).
        /// </summary>
        Task DispatchAsync(
            CancellationToken cancellationToken = default);
    }
}
