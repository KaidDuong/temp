using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;
using System.Collections.Generic;

namespace Rikkonbi.Core.Entities
{
    public class Category : BaseEntity, IAggregateRoot, IAuditable
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}