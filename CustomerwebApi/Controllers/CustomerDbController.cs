using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Model;
using System.Collections.Generic;

namespace Myapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerDbController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CustomerDbController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Customer>>> GetDatas()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> InsertDatas(Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.Name) || customer.Address == null)
            {
                return BadRequest("Invalid customer data");
            }
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            var existing = await _context.Customers.FindAsync(id);
            if (existing == null)
                return NotFound();

            // Update only the relevant fields
            existing.Name = customer.Name;
            existing.Address = customer.Address;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}