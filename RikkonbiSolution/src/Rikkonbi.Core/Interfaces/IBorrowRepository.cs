using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rikkonbi.Core.Interfaces
{
    public interface IBorrowRepository : IRepository<Borrow>, IRepositoryAsync<Borrow>
    {
        IEnumerable<DeviceBorrow> GetDeviceBorrowsBy(string userId);

        IEnumerable<Object> GetAllDeviceBorrows();
    }
}