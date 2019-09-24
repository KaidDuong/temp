using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Core.Entities
{
    public class Borrow : BaseEntity, IAggregateRoot, IAuditable
    {
        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime BorrowOn { set; get; }
        public DateTime ReturnOn { set; get; }

        public bool IsDeleted { set; get; }// deleted : true

        public int? DeviceId { get; set; }
        public bool Status { get; set; } // borrow : true , return : false
        public virtual Device Device { get; set; }
    }
}