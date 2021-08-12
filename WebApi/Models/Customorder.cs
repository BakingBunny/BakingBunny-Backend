using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class CustomOrder
    {
        public int Id { get; set; }
        public string RequestDescription { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Delivery { get; set; }
        public int UserId { get; set; }
        public int SizeId { get; set; }
        public int TasteId { get; set; }
        public int CakeTypeId { get; set; }

        public virtual User User { get; set; }
    }
}
