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
        Task<List<ProductDetail>> GetAll();
        Task<List<ProductDetail>> GetCakes();
        Task<List<ProductDetail>> GetDacquoises();
        Task<ProductDetail> GetProductById(int Id);
        void CreateOrder([FromBody] OrderDetail orderDetail);

        void CreateCustomOrder([FromBody] CustomOrder customOrder);
        int CalculateDeliveryFee(string postalCode);
    }
}
