using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Models
{
    public class OrderDetail
    {
        public List<SaleItem> saleItems { get; set; }
        public User user { get; set; }
        public OrderList orderList { get; set; }
    }
}
