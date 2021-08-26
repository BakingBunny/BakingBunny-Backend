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
    //[Route("api/[controller]")]
    //[Route("api/category")]
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

        [Route("api/[controller]")]
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

        [HttpGet("api/[controller]/{id}")]
        public ProductDetail GetProductById(int Id)
        {
            try
            {
                return _productRepository.GetProductById(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetProductById method");
                return null;
            }
        }

        [HttpGet("api/category/cakes")]
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

        [HttpGet("api/category/dacquoises")]
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

        [HttpPost("api/order")]
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

        [HttpPost("api/customorder")]
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

        [HttpGet("api/delivery/{postalCode}")]
        public double CalculateDeliveryFee(string postalCode)
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
