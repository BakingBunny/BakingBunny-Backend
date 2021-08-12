using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class OrderList
    {
        public int Id { get; set; }
        public float Subtotal { get; set; }
        public float DeliveryFee { get; set; }
        public float Total { get; set; }
        public bool Delivery { get; set; }
        public string Allergy { get; set; }
        public string Comment { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PickupDeliveryDate { get; set; }
        public int UserId { get; set; }
    }
}
