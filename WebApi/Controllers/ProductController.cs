using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCrud.Models;
using WebApiCrud.Repository;

namespace WebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public List<Product> GetAllProducts ()
        {
            return _productRepository.GetAll();
        }

        [HttpGet("cake")]
        public List<Product> GetCakes()
        {
            return _productRepository.GetCakes();
        }

        [HttpGet("dacq")]
        public List<Product> GetDacquoises()
        {
            return _productRepository.GetDacquoises();
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }
    }
}
