using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCrud.Models;

namespace WebApiCrud.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BakingbunnyContext _bakingbunnyContext;

        public ProductRepository(BakingbunnyContext bakingbunnyContext)
        {
            _bakingbunnyContext = bakingbunnyContext;
        }
        public Product GetProductById(int id)
        {
            return _bakingbunnyContext.Products.Where(s => s.Id == id).FirstOrDefault<Product>();
        }

        public List<Product> GetAll()
        {
            return _bakingbunnyContext.Products.ToList();
        }

        public List<Product> GetCakes()
        {
            return _bakingbunnyContext.Products.Where(s => s.CategoryId == 1).ToList();
        }

        public List<Product> GetDacquoises()
        {
            return _bakingbunnyContext.Products.Where(s => s.CategoryId == 2).ToList();
        }
    }
}
