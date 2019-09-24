using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;

namespace Rikkonbi.Infrastructure.Data
{
    public class OrderDetailRepository : EfRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(RikkonbiDbContext context) : base(context) { }
    }
}