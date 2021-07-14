using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class CustomOrderDetail
    {
        public string name { get; set; }
        public string exampleImage { get; set; }
        public string message { get; set; }
        public string comment { get; set; }
        public User user { get; set; }
        public Fruit fruit { get; set; }
        public Size size { get; set; }
        public Caketype caketype { get; set; }
    }
}
