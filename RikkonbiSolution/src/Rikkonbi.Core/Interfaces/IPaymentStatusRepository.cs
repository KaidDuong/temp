using Rikkonbi.Core.Entities;

namespace Rikkonbi.Core.Interfaces
{
    public interface IPaymentStatusRepository : IRepository<PaymentStatus>, IRepositoryAsync<PaymentStatus>
    {
    }
}
