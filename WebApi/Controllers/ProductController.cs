using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repository;
using WebApi.Services;

namespace WebApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMailService _mailService;

        public ProductController(IProductRepository productRepository, IMailService mailService)
        {
            _productRepository = productRepository;
            _mailService = mailService;
        }

        [HttpGet]
        public List<ProductDetail> GetAll()
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

        [HttpPost("order")]
        public void CreateOrder([FromBody] OrderDetail orderDetail)
        {
            _productRepository.CreateOrder(orderDetail);
        }

        [HttpPost("customorder")]
        public void CreateCustomOrder([FromBody] CustomOrder customOrder)
        {
            _productRepository.CreateCustomOrder(customOrder);
        }
    }
}
