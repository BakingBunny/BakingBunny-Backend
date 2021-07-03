using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCrud.Models
{
    public partial class Product
    {
        public Product()
        {
            Saleitems = new HashSet<Saleitem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ProductImage { get; set; }
        public string Comment { get; set; }
        public ulong Active { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Saleitem> Saleitems { get; set; }
    }
}
