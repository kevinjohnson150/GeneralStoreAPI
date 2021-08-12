using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.Data_POCOS.Products;
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
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Post(Product product)
        {
            if (product is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{product.Name} was added.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllProductsBySKU(string sku)
        {
            var products = await _context.Products.FindAsync(sku);
            if (products is null)
            {
                return NotFound();
            }
            return Ok(products);  
        }
    }
}
