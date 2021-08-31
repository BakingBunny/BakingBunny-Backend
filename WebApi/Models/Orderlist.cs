using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApi.Models
{
    public partial class OrderList
    {
        public int Id { get; set; }
        [Required]
        public float Subtotal { get; set; }
        [Required]
        public float DeliveryFee { get; set; }
        [Required]
        public float Total { get; set; }
        [Required]
        public bool Delivery { get; set; }
        public string Allergy { get; set; }
        public string Comment { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime PickupDeliveryDate { get; set; }
        public int UserId { get; set; }
    }
}
