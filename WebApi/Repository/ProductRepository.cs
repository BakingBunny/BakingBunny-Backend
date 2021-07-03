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
        public async Task<Product> Get(int id)
        {
            return await _bakingbunnyContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _bakingbunnyContext.Products.ToListAsync();
        }
    }
}
