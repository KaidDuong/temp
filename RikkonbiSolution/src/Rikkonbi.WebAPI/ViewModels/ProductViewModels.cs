using System.ComponentModel.DataAnnotations;

namespace Rikkonbi.WebAPI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string QrCodeImageUrl { get; set; }
        public int MaxOrderQuantity { get; set; }
    }

    public class CreateProductViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }

    public class EditProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? DeviceCategoryId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }
}