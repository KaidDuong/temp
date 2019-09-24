using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rikkonbi.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>, IRepositoryAsync<Order>
    {
        List<Order> ListOrder(Expression<Func<Order, bool>> predicate);
    }
}
