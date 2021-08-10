using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private static ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMailService _mailService;

        public ProductController(IProductRepository productRepository, IMailService mailService, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _mailService = mailService;
            _logger = logger;
        }

        [HttpGet]
        public List<ProductDetail> GetAll()
        {
            try
            {
                return _productRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetAll method");
                return null;
            }
        }

        [HttpGet("cake")]
        public List<ProductDetail> GetCakes()
        {
            try
            {
                return _productRepository.GetCakes();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetCakes method");
                return null;
            }
        }

        [HttpGet("dacq")]
        public List<ProductDetail> GetDacquoises()
        {
            try
            {
                return _productRepository.GetDacquoises();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetDacquoises method");
                return null;
            }
            
        }

        [HttpPost("order")]
        public void CreateOrder([FromBody] OrderDetail orderDetail)
        {
            try
            {
                _productRepository.CreateOrder(orderDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing CreateOrder method");
            } 
        }

        [HttpPost("customorder")]
        public void CreateCustomOrder([FromBody] CustomOrder customOrder)
        {
            try
            {
                _productRepository.CreateCustomOrder(customOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing CustomOrder method");
            }
        }

        [HttpPost("delivery")]
        public double CalculateDeliveryFee([FromBody] string postalCode)
        {
            try
            {
                return _productRepository.CalculateDeliveryFee(postalCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing CalculateDeliveryFee method");
                return -1;
            }
        }
    }
}
