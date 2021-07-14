using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Fruit
    {
        public Fruit()
        {
            //Customorders = new HashSet<Customorder>();
            //Saleitems = new HashSet<Saleitem>();
        }

        public int Id { get; set; }
        public string FruitName { get; set; }

        //public virtual ICollection<Customorder> Customorders { get; set; }
        //public virtual ICollection<Saleitem> Saleitems { get; set; }
    }
}
