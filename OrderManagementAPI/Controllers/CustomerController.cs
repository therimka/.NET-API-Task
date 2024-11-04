using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data; 
using OrderManagementAPI.Models; 
using OrderManagementAPI.DTOs; 

namespace OrderManagementAPI.Controllers 
{
    [Route("api/[controller]")]
    [ApiController] 
    public class CustomerController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;

        public CustomerController(OrderManagementDbContext context) 
        {
            _context = context; 
        }

        [HttpPost] 
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDto customerDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

         
            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                PhoneNumber = customerDto.PhoneNumber,
                Address = customerDto.Address
            };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

           
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customer.ToListAsync(); 
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id); 

            if (customer == null)
            {
                return NotFound(); 
            }

            return customer; 
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> PutCustomer(int id, CustomerDto customerDto)
        {
            var customer = await _context.Customer.FindAsync(id); 
            if (customer == null)
            {
                return NotFound(); 
            }

          
            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.PhoneNumber = customerDto.PhoneNumber;
            customer.Address = customerDto.Address;

            _context.Entry(customer).State = EntityState.Modified; 

            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!CustomerExists(id)) 
                {
                    return NotFound(); 
                }
                else
                {
                    throw; 
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer); 
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id) 
        {
            return _context.Customer.Any(e => e.CustomerID == id); 
        }
    }
}
