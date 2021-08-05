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
        List<ProductDetail> GetAll();
        List<ProductDetail> GetCakes();
        List<ProductDetail> GetDacquoises();
        List<Size> GetSizes();
        List<Taste> GetTastes();
        void CreateOrder([FromBody] OrderDetail orderDetail);

        void CreateCustomOrder([FromBody] CustomOrder customOrder);
    }
}
