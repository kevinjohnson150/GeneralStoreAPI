using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.Data_POCOS.Customers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // POST create customer
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer is null)
            {
                return BadRequest("Your request cannot be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerEntity = await _context.Customers.FindAsync(customer.Id);
            if (customerEntity is null)
                return BadRequest($"The customer with the ID of {customer.Id} does not exist.");

            _context.Customers.Add(customer);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You have added {customerEntity.FullName} successfully");

            return InternalServerError();
        }

        // GET all customers
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer()
        {
            var customer = await _context.Customers.ToListAsync();

            return Ok(customer);
        }

        // Get customer by ID
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerById([FromUri] int id)
        {
            var customerId = await _context.Customers.FindAsync(id);
            if (customerId is null)
            {
                return NotFound();
            }
            return Ok(customerId);
            
        }

        // Update an existing customer by ID
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomerById([FromUri] int id, [FromBody] Customer newCustomer)
        {
            if (id != newCustomer.Id)
            {
                return BadRequest("Invalid Id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newCustomer is null)
            {
                return BadRequest("Empty Request Error");
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer is null)
            {
                return NotFound();
            }

            customer.FirstName = newCustomer.FirstName;
            customer.LastName = newCustomer.LastName;
            customer.Id = newCustomer.Id;

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Customer was updated.");
            }
            else
            {
                return InternalServerError();
            }
        }
        
        // Delete customer by ID
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomerById([FromUri] int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Customer was successfully removed from the database.");
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
