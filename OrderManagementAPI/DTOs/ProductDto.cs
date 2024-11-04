using System.ComponentModel.DataAnnotations;

namespace OrderManagementAPI.DTOs
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set; } = 0; 

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }
    }
}
