using Rikkonbi.Core.Entities;

namespace Rikkonbi.Core.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>, IRepositoryAsync<OrderDetail>
    {
    }
}
