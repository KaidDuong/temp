using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Rikkonbi.WebAPI.ViewModels
{
    public class DeviceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int DeviceCategoryId { get; set; }
        public string QrCodeImageUrl { get; set; }
        public bool IsBorrowed { set; get; }
        public bool IsDeleted { set; get; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CreateDeviceViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public int? DeviceCategoryId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }

    public class EditDeviceViewModel
    {
        [Required]
        public int Id { set; get; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public int? DeviceCategoryId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }
}