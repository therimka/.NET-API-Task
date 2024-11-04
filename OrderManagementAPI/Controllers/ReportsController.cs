using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using OrderManagementAPI.Data; 
using OrderManagementAPI.DTOs; 


namespace OrderManagementAPI.Controllers 
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class ReportsController : ControllerBase
    {
        private readonly OrderManagementDbContext _context; 

        public ReportsController(OrderManagementDbContext context) 
        {
            _context = context;
        }

        [HttpGet("daily-orders-summary")] 
        public IActionResult GetDailyOrdersSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
           
            var completedOrders = _context.Order
                .Include(o => o.OrderItems) 
                .Where(o => o.Status == "Completed" && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToList();

          
            var totalOrders = completedOrders.Count; 
            var totalRevenue = completedOrders.Sum(o => o.TotalPrice);

          
            var summaryDto = new DailyOrdersSummaryDto
            {
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                CompletedOrders = totalOrders 
            };

            return Ok(summaryDto); 
        }

        [HttpGet("top-customers")] 
        public IActionResult GetTopCustomers(DateTime startDate, DateTime endDate)
        {
            var topCustomers = _context.Order
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate && o.Status == "Completed")
                .SelectMany(o => o.OrderItems.Select(oi => new
                {
                    CustomerID = o.CustomerID, 
                    TotalSpent = oi.Quantity * oi.UnitPrice 
                }))
                .GroupBy(x => x.CustomerID) 
                .Select(g => new
                {
                    CustomerID = g.Key, 
                    TotalSpent = g.Sum(x => x.TotalSpent) 
                })
                .OrderByDescending(c => c.TotalSpent) 
                .ToList(); 

            return Ok(topCustomers);
        }
    }
}
