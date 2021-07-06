using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCrud.Models;

namespace WebApiCrud.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetProductById(int id);

        List<Product> GetCakes();
        List<Product> GetDacquoises();
    }
}
