using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;

namespace Rikkonbi.Core.Entities
{
    public class Product : BaseEntity, IAggregateRoot, IAuditable
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

		public string QrCodeContent { get; set; }

        public string QrCodeImageUrl { get; set; }

        public string Description { get; set; }

        public int MaxOrderQuantity { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public virtual Category Category { get; set; }
    }
}