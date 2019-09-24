using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Infrastructure.Data
{
    public class DeviceCategoryRepository : EfRepository<DeviceCategory>, IDeviceCategoryRepository
    {
        public DeviceCategoryRepository(RikkonbiDbContext context) : base(context)
        {
        }
    }
}