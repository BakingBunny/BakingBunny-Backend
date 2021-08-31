using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApi.Models
{
    public partial class SaleItem
    {
        public SaleItem()
        {
        }

        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public float? Discount { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int SizeId { get; set; }
        [Required]
        public int TasteId { get; set; }
        public int OrderListId { get; set; }
    }
}
