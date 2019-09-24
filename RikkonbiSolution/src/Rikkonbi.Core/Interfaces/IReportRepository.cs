using Rikkonbi.Core.Aggregates.OrderReport;
using System;
using System.Collections.Generic;

namespace Rikkonbi.Core.Interfaces
{
    public interface IReportRepository
    {
        List<UnpaidOrder> GetUnpaidList(DateTime currentDate);
        bool UpdatePaymentStatus(List<int> orderIds);
    }
}