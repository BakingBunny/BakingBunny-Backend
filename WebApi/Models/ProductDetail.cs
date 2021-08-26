using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string Comment { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Taste> TasteList { get; set; }
        public List<Size> SizeList { get; set; }
        public List<CakeType> CakeTypeList { get; set; }
    }
}
