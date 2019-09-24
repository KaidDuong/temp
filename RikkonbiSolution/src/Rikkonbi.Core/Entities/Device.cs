using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rikkonbi.Core.Entities
{
    public class Device : BaseEntity, IAggregateRoot, IAuditable
    {
        public string Name { get; set; }

        public string ImageUrl { set; get; }
        public string Description { get; set; }

        public string QrCodeContent { get; set; }
        public string QrCodeImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsBorrowed { get; set; }
        public bool IsDeleted { get; set; }

        public int? DeviceCategoryId { get; set; }
        public virtual DeviceCategory DeviceCategory { set; get; }

        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}