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
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_productRepository.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetAll method");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/[controller]/{id}")]
        public IActionResult GetProductById(int Id)
        {
            try
            {
                return Ok(_productRepository.GetProductById(Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetProductById method");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/category/cakes")]
        public IActionResult GetCakes()
        {
            try
            {
                return Ok(_productRepository.GetCakes());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetCakes method");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("api/category/dacquoises")]
        public IActionResult GetDacquoises()
        {
            try
            {
                return Ok(_productRepository.GetDacquoises());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing GetDacquoises method");
                return StatusCode(500, "Internal server error");
            }
            
        }

        [HttpPost("api/order")]
        public IActionResult CreateOrder([FromBody] OrderDetail orderDetail)
        {
            try
            {
                if (orderDetail == null)
                {
                    _logger.LogError("OrderDetail object is null");
                    return BadRequest("Internal server error");
                }
                else if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid OrderDetail object");
                    return BadRequest("Internal server error");
                }
                _productRepository.CreateOrder(orderDetail);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing CreateOrder method");
                return BadRequest("Internal server error");
            } 
        }

        [HttpPost("api/customorder")]
        public IActionResult CreateCustomOrder([FromBody] CustomOrder customOrder)
        {
            try
            {
                if (customOrder == null)
                {
                    _logger.LogError("CustomOrder object is null");
                    return BadRequest("Internal server error");
                }
                else if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid CustomOrder object");
                    return BadRequest("Internal server error");
                }
                _productRepository.CreateCustomOrder(customOrder);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while executing CustomOrder method");
                return BadRequest("Internal server error");
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
