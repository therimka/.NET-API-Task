using System.Text.Json.Serialization; 

namespace OrderManagementAPI.Models 
{
    public class Order 
    {
        public int OrderID { get; set; } 
        public int CustomerID { get; set; } 

        public string Status { get; set; } 

        public DateTime OrderDate { get; set; } 

        
        public decimal TotalPrice => OrderItems?.Sum(oi => oi.TotalPrice) ?? 0; 

       
        [JsonIgnore]
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }
}
