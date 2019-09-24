using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Core.Interfaces
{
    public interface IDeviceRepository : IRepository<Device>, IRepositoryAsync<Device>
    {
    }
}