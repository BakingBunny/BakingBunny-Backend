using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCrud.Models
{
    public partial class Orderlist
    {
        public int Id { get; set; }
        public float Subtotal { get; set; }
        public float DeliveryFee { get; set; }
        public float Total { get; set; }
        public ulong Delivery { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public int SaleItemId { get; set; }

        public virtual Saleitem SaleItem { get; set; }
        public virtual User User { get; set; }
    }
}
