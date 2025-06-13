using System.Threading.Tasks;

namespace LibraryManagement.Domain.Common
{
    public interface IPublisher
    {
        Task PublishAsync(IDomainEvent domainEvent);
    }
}
