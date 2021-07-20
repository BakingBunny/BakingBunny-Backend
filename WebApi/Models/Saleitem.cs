using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class SaleItem
    {
        public SaleItem()
        {
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public float? Discount { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int FruitId { get; set; }
        public int OrderListId { get; set; }
    }
}
