using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data; 
using OrderManagementAPI.Models; 
using OrderManagementAPI.DTOs; 


namespace OrderManagementAPI.Controllers 
{
    [Route("api/[controller]")] 
    [ApiController] 
    public class OrderController : ControllerBase
    {
        private readonly OrderManagementDbContext _context; 

        public OrderController(OrderManagementDbContext context) 
        {
            _context = context; 
        }
        [HttpPost] 
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            
            var orderItems = new List<OrderItem>();
            decimal totalPrice = 0; 

            foreach (var item in orderDto.OrderItems) 
            {
                var product = await _context.Product.FindAsync(item.ProductID); 
                if (product == null || product.StockQuantity < item.Quantity)
                {
                    return BadRequest($"Insufficient stock for product ID {item.ProductID}."); 
                }

                
                product.StockQuantity -= item.Quantity;

                
                var orderItem = new OrderItem
                {
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    ProductID = item.ProductID 
                };

                orderItems.Add(orderItem); 
                totalPrice += orderItem.TotalPrice; 
            }

            
            var order = new Order
            {
                CustomerID = orderDto.CustomerID,
                Status = "Pending",
                OrderDate = DateTime.UtcNow, 
                OrderItems = orderItems
            };

            _context.Order.Add(order); 
            await _context.SaveChangesAsync(); 

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order); 
        }


        [HttpGet("{id}")] 
        public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
        {
            var order = await _context.Order
                .Include(o => o.OrderItems) 
                .FirstOrDefaultAsync(o => o.OrderID == id); 

            if (order == null)
            {
                return NotFound(); 
            }

            
            var orderResponse = new OrderResponseDto
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    OrderItemID = oi.OrderItemID,
                    ProductID = oi.ProductID,
                    Quantity = oi.Quantity,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            };

            return orderResponse; 
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
        {
            
            var orders = await _context.Order
                .Include(o => o.OrderItems) 
                .ToListAsync(); 

            
            var orderResponseDtos = orders.Select(order => new OrderResponseDto
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                Status = order.Status,
                TotalPrice = order.TotalPrice, 
                OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    OrderItemID = oi.OrderItemID,
                    ProductID = oi.ProductID,
                    Quantity = oi.Quantity,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            }).ToList();

            return Ok(orderResponseDtos); 
        }

        [HttpPatch("{orderId}/status")] 
        public IActionResult UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            var order = _context.Order.Find(orderId); 
            if (order == null)
            {
                return NotFound(); 
            }

            order.Status = status; 
            _context.SaveChanges(); 

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            
            _context.OrderItem.RemoveRange(order.OrderItems);
            _context.Order.Remove(order);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
