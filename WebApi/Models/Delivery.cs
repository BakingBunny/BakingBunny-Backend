using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string PostalCode { get; set; }
        public int Distance { get; set; }
        public int DeliveryFee { get; set; }
    }
}
