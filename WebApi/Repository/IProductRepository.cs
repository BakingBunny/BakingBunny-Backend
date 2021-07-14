using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IProductRepository
    {
        //List<Product> GetAll();
        Product GetProductById(int id);

        List<Product> GetCakes();
        List<Product> GetDacquoises();
        List<Product> GetDacquoisesCombo();
        void CreateOrder([FromBody] OrderDetail orderDetail);

        void CreateCustomOrder([FromBody] CustomOrderDetail customOrderDetail);
    }
}
