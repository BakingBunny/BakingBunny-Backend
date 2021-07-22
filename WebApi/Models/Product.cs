using System;
using System.Collections.Generic;
using WebApi.Models;

#nullable disable

namespace WebApi.Models
{
    public partial class Product
    {
        public Product()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public int CategoryId { get; set; }

    }
}
