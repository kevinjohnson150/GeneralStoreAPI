using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models.Data_POCOS.Products
{
    public class Product
    {
        [Key]
        public string SKU { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        [Required]
        public int NumberInventory { get; set; }

        [Required]
        public bool IsInStock { get; }

    }
}