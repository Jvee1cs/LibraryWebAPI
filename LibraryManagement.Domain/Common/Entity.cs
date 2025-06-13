//namespace LibraryManagement.Domain.Common
//{
//    public abstract class Entity
//    {
//        public List<IDomainEvent> DomainEvents { get; set; } = [];

//        public void RaiseDomainEvent(IDomainEvent domainEvent)
//        {
//            DomainEvents.Add(domainEvent);
//        }

//        public void ClearDomainEvents()
//        {
//            DomainEvents.Clear();
//        }
//    }

//}
namespace LibraryManagement.Domain.Common
{
    public abstract class Entity
    {
        // Keep the mutable list private…
        private readonly List<IDomainEvent> _domainEvents = [];

        // …and expose a read‑only view.
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        // Usually called by the dispatcher after publishing.
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
