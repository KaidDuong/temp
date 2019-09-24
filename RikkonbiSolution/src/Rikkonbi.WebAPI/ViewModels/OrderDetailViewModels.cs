using System.ComponentModel.DataAnnotations;

namespace Rikkonbi.WebAPI.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CreateOrderDetailViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

    public class EditOrderDetailViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}