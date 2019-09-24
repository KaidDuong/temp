using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rikkonbi.WebAPI.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int RegionId { get; set; }

        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0;

                foreach (var item in Items)
                {
                    totalAmount += item.Quantity * item.Price;
                }

                return totalAmount;
            }
        }

        public DateTime OrderDate { get; set; }

        public int PaymentStatusId { get; set; }

        public string PaymentStatusName { get; set; }

        public List<OrderDetailViewModel> Items { get; set; }
    }

    public class OrderHistoryViewModel
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0;

                foreach (var item in Items)
                {
                    totalAmount += item.Quantity * item.Price;
                }

                return totalAmount;
            }
        }

        public DateTime OrderDate { get; set; }

        public int PaymentStatusId { get; set; }

        public string PaymentStatusName { get; set; }

        public List<OrderDetailViewModel> Items { get; set; }
    }

    public class CreateOrderViewModel
    {
        [Required]
        public int RegionId { get; set; }

        [Required]
        public List<CreateOrderDetailViewModel> Items { get; set; }
    }

    public class EditOrderViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserId { get; set; }

        [Required]
        public int RegionId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int PaymentStatusId { get; set; }

        [Required]
        public List<EditOrderDetailViewModel> Items { get; set; }
    }

    public class OrderFilterViewModel
    {
        public int? PaymentStatusId { get; set; }
        public DateTime? OrderDateFrom { get; set; }
        public DateTime? OrderDateTo { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
    }
}