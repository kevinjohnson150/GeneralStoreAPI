using GeneralStoreAPI.Models.Data_POCOS.Customer;
using GeneralStoreAPI.Models.Data_POCOS.Product;
using GeneralStoreAPI.Models.Data_POCOS.Transaction;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models.GeneralStoreAPIDBContext
{
    public class GeneralStoreAPIDbContext : DbContext
    {
        public GeneralStoreAPIDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}