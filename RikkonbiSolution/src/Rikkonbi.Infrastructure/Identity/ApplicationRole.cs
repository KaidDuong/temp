using Microsoft.AspNetCore.Identity;
using Rikkonbi.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Rikkonbi.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole, IRole<string>, IAuditable
    {
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}