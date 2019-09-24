using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Core.Interfaces
{
    public interface IDeviceCategoryRepository : IRepository<DeviceCategory>, IRepositoryAsync<DeviceCategory>
    {
    }
}