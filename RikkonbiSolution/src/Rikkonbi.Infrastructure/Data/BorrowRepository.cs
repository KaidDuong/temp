using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Rikkonbi.Infrastructure.Data
{
    public class BorrowRepository : EfRepository<Borrow>, IBorrowRepository
    {
        public BorrowRepository(RikkonbiDbContext context) : base(context)
        {
        }

        public IEnumerable<DeviceBorrow> GetDeviceBorrowsBy(string userId)
        {
            return _dbContext.Devices.GroupJoin(_dbContext.Borrows.Where(b => b.UserId == userId),
                                                 device => device.Id,
                                                 borrow => borrow.DeviceId,
                                                 (device, borrowColection) =>
                                                 new DeviceBorrow
                                                 {
                                                     UserName = _dbContext.Users.Where(us => us.Id == userId).Select(us => us.UserName).ToString(),
                                                     DeviceName = device.Name,
                                                     DeviceBorrows = borrowColection
                                                 }
                );
        }

        public IEnumerable<Object> GetAllDeviceBorrows()
        {
            return from u in _dbContext.UserClaims
                   join b in _dbContext.Borrows
                   on u.UserId equals b.UserId
                   join d in _dbContext.Devices
                   on b.DeviceId equals d.Id
                   select new
                   {
                       UserName = _dbContext.Users.Where(us => us.Id == u.UserId).Select(us => us.UserName).ToString(),
                       DeviceName = d.Name,
                       DeviceBorrows = b
                   };
        }
    }
}