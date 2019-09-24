using Microsoft.EntityFrameworkCore;
using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rikkonbi.Infrastructure.Data
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(RikkonbiDbContext context) : base(context) { }

        public List<Order> ListOrder(Expression<Func<Order, bool>> predicate)
        {
            return _dbContext.Orders
                .Include(x => x.Items)
                .Include(x => x.PaymentStatus)
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToList();
        }
    }
}