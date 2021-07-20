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

        [HttpGet("size")]
        public List<Size> GetSizes()
        {
            return _productRepository.GetSizes();
        }

        [HttpGet("fruit")]
        public List<Fruit> GetFruits()
        {
            return _productRepository.GetFruits();
        }

        [HttpGet("caketype")]
        public List<Caketype> GetCaketypes()
        {
            return _productRepository.GetCaketypes();
        }

        [HttpPost("Order")]
        public void CreateOrder([FromBody] OrderDetail orderDetail)
        {
#if (DEBUG)
            _productRepository.CreateOrder(orderDetail);
#else
            _mailService.SendInternalEmailRegularAsync(orderDetail);
            _mailService.SendEmailToClientRegularAsync(orderDetail);
#endif
        }

        [HttpPost("CustomOrder")]
        public void CreateCustomOrder([FromBody] CustomOrder customOrder)
        {
#if (DEBUG)
            _productRepository.CreateCustomOrder(customOrder);
#else
            _mailService.SendInternalEmailCustomAsync(customOrder);
            _mailService.SendEmailToClientCustomAsync(customOrder);
#endif
        }
    }
}
