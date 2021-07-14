using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Saleitem
    {
        public Saleitem()
        {
            //Orderlists = new HashSet<Orderlist>();
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public float? Discount { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int FruitId { get; set; }
        public int OrderListId { get; set; }

        public virtual Fruit Fruit { get; set; }
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        //public virtual ICollection<Orderlist> Orderlists { get; set; }
        public virtual Orderlist Orderlist { get; set; }
    }
}
