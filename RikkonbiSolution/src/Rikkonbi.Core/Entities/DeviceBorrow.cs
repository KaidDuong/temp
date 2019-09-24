using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Core.Entities
{
    public class DeviceBorrow
    {
        public string UserName { set; get; }
        public string DeviceName { set; get; }
        public IEnumerable<Borrow> DeviceBorrows { set; get; }
    }
}