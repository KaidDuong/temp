using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;

namespace Rikkonbi.Core.Entities
{
    public class OrderDetail : BaseEntity, IAggregateRoot
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}