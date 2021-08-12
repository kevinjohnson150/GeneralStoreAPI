using GeneralStoreAPI.Models;
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
            if (transaction is null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            _context.Transactions.Add(transaction);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"{transaction.Id} was added.");
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
        public async Task<IHttpActionResult> GetAllTransactionsByID([FromUri] string sku)
        {
            var transaction = await _context.Transactions.FindAsync(sku);
            if (transaction is null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
    }
}
