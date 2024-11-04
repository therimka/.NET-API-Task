using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using OrderManagementAPI.Data; 
using OrderManagementAPI.Models; 
using OrderManagementAPI.DTOs; 


namespace OrderManagementAPI.Controllers 
{
    [Route("api/[controller]")] 
    [ApiController] 
    public class ProductController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;

        public ProductController(OrderManagementDbContext context) 
        {
            _context = context; 
        }

        [HttpPost] 
        public async Task<ActionResult<Product>> CreateProduct(ProductDto productDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                Description = productDto.Description
            };

            _context.Product.Add(product); 
            await _context.SaveChangesAsync(); 

            
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id); 
            if (product == null)
            {
                return NotFound(); 
            }

            return product; 
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Product.ToListAsync(); 
        }
    }
}
