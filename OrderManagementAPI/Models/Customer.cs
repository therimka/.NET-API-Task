using System.ComponentModel.DataAnnotations; 

namespace OrderManagementAPI.Models 
{
    public class Customer 
    {
        public int CustomerID { get; set; } 

        [Required(ErrorMessage = "Name is required")] 
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")] 
        public string Name { get; set; } 

        [Required(ErrorMessage = "Email is required")] 
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")] 
        public string Email { get; set; } 

        [Required(ErrorMessage = "Phone number is required.")] 
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]
        public string PhoneNumber { get; set; } 

        [Required(ErrorMessage = "Address is required.")] 
        [MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters.")] 
        public string Address { get; set; }
    }
}