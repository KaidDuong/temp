using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rikkonbi.WebAPI.ViewModels
{
    public class DeviceBorrowViewModel
    {
        public string DeviceName { set; get; }

        public IEnumerable<Borrow> GetBorrows { set; get; }
    }

    public class CreateDeviceBorrowViewModel
    {
        [Required]
        public int DeviceId { set; get; }

        public DateTime ReturnOn { set; get; }
    }
}