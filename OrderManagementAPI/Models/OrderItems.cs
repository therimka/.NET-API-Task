using System.ComponentModel.DataAnnotations; 
using System.Text.Json.Serialization; 

namespace OrderManagementAPI.Models 
{
    public class OrderItem 
    {
        [Key] 
        public int OrderItemID { get; set; } 

        public int OrderID { get; set; }

       
        [JsonIgnore] 
        public Order Order { get; set; } 

        public int ProductID { get; set; } 

     
        [JsonIgnore] 
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")] 
        public int Quantity { get; set; } 

        [Required(ErrorMessage = "Unit price is required.")] 
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")] 
        public decimal UnitPrice { get; set; } 

        
        public decimal TotalPrice => Quantity * UnitPrice; 
    }
}
