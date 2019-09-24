using Rikkonbi.Core.SharedKernel;

namespace Rikkonbi.Core.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}