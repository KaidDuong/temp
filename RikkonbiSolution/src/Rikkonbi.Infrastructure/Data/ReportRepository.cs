using Microsoft.EntityFrameworkCore;
using Rikkonbi.Core.Aggregates.OrderReport;
using Rikkonbi.Core.Entities;
using Rikkonbi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Rikkonbi.Core.Interfaces
{
    public class ReportRepository : EfRepository<Order>, IReportRepository
    {
        public ReportRepository(RikkonbiDbContext context) : base(context) { }

        public List<UnpaidOrder> GetUnpaidList(DateTime currentDate)
        {
            string sql = @"SELECT
	                            U.FullName
	                          , U.Email
	                          , COALESCE((SELECT SUM(Price * Quantity)
				                          FROM OrderDetails OD
				                          WHERE OD.OrderId = O.Id), 0) as TotalAmount
	                          , O.Id as OrderId
	                          , O.OrderDate
	                          , '' as Note
                           FROM Orders O
                           INNER JOIN AspNetUsers U
	                           ON O.UserId = U.Id
                           WHERE O.PaymentStatusId = 1";
            var unpaidList = _dbContext.UnpaidOrders.FromSql(sql).Where(x => x.OrderDate <= currentDate).ToList();
            return unpaidList;
        }

        public bool UpdatePaymentStatus(List<int> orderIds)
        {
            string sql = $"UPDATE Orders SET PaymentStatusId = 2 WHERE Id IN ({string.Join(",", orderIds.ToArray())})";
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            int affectedRows = _dbContext.Database.ExecuteSqlCommand(sql);
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
            return affectedRows > 0;
        }
    }
}