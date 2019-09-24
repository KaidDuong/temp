using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;

namespace Rikkonbi.Core.Entities
{
    public class PaymentStatus : BaseEntity, IAggregateRoot, IAuditable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
    }
}