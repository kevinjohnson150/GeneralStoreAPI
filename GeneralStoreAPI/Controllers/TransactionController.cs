using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.Data_POCOS.Products;
using GeneralStoreAPI.Models.Data_POCOS.Transactions;
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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Post(Transaction transaction)
        {
            Product product = await _context.Products.FindAsync(transaction.ProductSKU);

            if (product is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productName = await _context.Products.FindAsync(transaction.Product.Name);
            if (productName is null)
                return BadRequest($"The product with the name {transaction.Product.Name} does not exist.");

            var stock = await _context.Products.FindAsync(transaction.Product.IsInStock);
            if (stock is null)
                return BadRequest($"The product requested {transaction.Product.IsInStock} is not available.");

            if (stock != null)
                return Ok($"The product requested {transaction.Product.IsInStock} is available.");

            if (transaction.ItemCount > product.NumberInventory)
                return BadRequest($"There is not enough invetory avaiilable. The  amount of products available is {product.NumberInventory}");

            _context.Transactions.Add(transaction);
            product.NumberInventory = product.NumberInventory - transaction.ItemCount;

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{transaction.Product} was purchased.");
            }
            else
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllTransaction()
        {
            var transaction = await _context.Transactions.ToListAsync();

            return Ok(transaction);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllTransactionsByID([FromUri] int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction is null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
    }
}
