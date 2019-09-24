using Rikkonbi.Core.SharedKernel;

namespace Rikkonbi.Core.Interfaces
{
    public interface IHandle<T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}