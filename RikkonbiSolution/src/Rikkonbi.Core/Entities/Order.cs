using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;
using System.Collections.Generic;

namespace Rikkonbi.Core.Entities
{
    public class Order : BaseEntity, IAggregateRoot, IAuditable
    {
        public string UserId { get; set; }

        public int RegionId { get; set; }

        public DateTime OrderDate { get; set; }

        public int PaymentStatusId { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public virtual ICollection<OrderDetail> Items { get; set; }

        public virtual PaymentStatus PaymentStatus { get; set; }
    }
}