using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Infrastructure.Data
{
    public class DeviceRepository : EfRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(RikkonbiDbContext context) : base(context)
        {
        }
    }
}