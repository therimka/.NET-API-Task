namespace OrderManagementAPI.DTOs 
{
    public class OrderResponseDto 
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; } 
        public List<OrderItemResponseDto> OrderItems { get; set; } 
    }

    public class OrderItemResponseDto 
    {
        public int OrderItemID { get; set; } 
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; } 
    }
}
