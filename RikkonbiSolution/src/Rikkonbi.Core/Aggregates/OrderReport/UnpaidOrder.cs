using Rikkonbi.Core.Interfaces;
using System;

namespace Rikkonbi.Core.Aggregates.OrderReport
{
    public class UnpaidOrder : IAggregateRoot
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Note { get; set; }
    }
}