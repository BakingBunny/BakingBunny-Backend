using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Models
{
    public class OrderDetail
    {
        public List<Product> productList { get; set; }
        public User user { get; set; }
        public Orderlist orderlist { get; set; }
        public Fruit fruit { get; set; }
        public Size size { get; set; }
        public int quantity { get; set; }
        public double discount { get; set; }
    }
}
