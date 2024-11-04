namespace OrderManagementAPI.DTOs 
{
    public class DailyOrdersSummaryDto 
    {
        public int TotalOrders { get; set; } 
        public decimal TotalRevenue { get; set; } 
        public int CompletedOrders { get; set; } 
    }
}
