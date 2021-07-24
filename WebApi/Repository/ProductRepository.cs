using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BakingbunnyContext _bakingbunnyContext;
        private readonly IMailService _mailService;

        public ProductRepository(BakingbunnyContext bakingbunnyContext, IMailService mailService)
        {
            _bakingbunnyContext = bakingbunnyContext;
            _mailService = mailService;
        }

        /// <summary>
        /// Get all product details for frontend
        /// </summary>
        /// <returns></returns>
        public List<ProductDetail> GetAll()
        {
            List<ProductDetail> productDetailList = new List <ProductDetail>();
            List<Product> productList = GetProducts();
            List<Taste> tasteList = GetTastes();
            List<Size> sizeList = GetSizes();

            ProductDetail productDetail;
            foreach (Product p in productList)
            {
                productDetail = new ProductDetail();
                productDetail.ProductId = p.Id;
                productDetail.ProductName = p.Name;
                productDetail.Price = p.Price;
                productDetail.Description = p.Description;
                productDetail.ProductImage = p.ProductImage;
                productDetail.Comment = p.Comment;
                productDetail.CategoryId = p.CategoryId;

                // Taste
                if (p.Id == 29)
                    productDetail.TasteList = tasteList.Where(t => t.Id >= 4 && t.Id <= 9).ToList();
                else if (p.Id == 2)
                    productDetail.TasteList = tasteList.Where(t => t.Id <= 3).ToList();
                else
                    productDetail.TasteList = new List<Taste>();

                // Size
                if (p.CategoryId == 1)
                    productDetail.SizeList = sizeList;
                else
                    productDetail.SizeList = new List<Size>();

                productDetailList.Add(productDetail);
            }


            return productDetailList;
        }

        /// <summary>
        /// Retrieve all sizes
        /// </summary>
        /// <returns>List<Size></returns>
        public List<Size> GetSizes()
        {
            return _bakingbunnyContext.Size.ToList();
        }

        /// <summary>
        /// Retrieve all fruits
        /// </summary>
        /// <returns>List<Fruit></returns>
        public List<Taste> GetTastes()
        {
            return _bakingbunnyContext.Taste.ToList();
        }

        /// <summary>
        /// Just for email method to provide product data.
        /// </summary>
        /// <returns>List<Product></returns>
        private List<Product> GetProducts()
        {
            return _bakingbunnyContext.Product.Where(s => s.Active).ToList();
        }

        /// <summary>
        /// To faciliate POST method CreateOrder.
        /// </summary>
        /// <param name="orderDetail">OrderDetail class contains all necessary objects to accommodate regular cake order in db.</param>
        public void CreateOrder([FromBody] OrderDetail orderDetail)
        {
            using (var dbContext = new BakingbunnyContext())
            {
                User user = new User()
                {
                    Firstname = orderDetail.user.Firstname,
                    Lastname = orderDetail.user.Lastname,
                    Email = orderDetail.user.Email,
                    Phone = orderDetail.user.Phone,
                    Address = orderDetail.user.Address,
                    City = orderDetail.user.City,
                    PostalCode = orderDetail.user.PostalCode,
                };
                dbContext.Add(user);
                dbContext.SaveChanges();

                double subTotal = 0;
                foreach (SaleItem saleItem in orderDetail.saleItems)
                {
                    Product p = dbContext.Product.Where(s => s.Id == saleItem.ProductId).FirstOrDefault();
                    subTotal += p.Price * saleItem.Quantity;
                }

                OrderList orderList = new OrderList()
                {
                    PickupDeliveryDate = orderDetail.orderList.PickupDeliveryDate,
                    Delivery = orderDetail.orderList.Delivery,
                    DeliveryFee = orderDetail.orderList.DeliveryFee,
                    OrderDate = DateTime.Now,
                    Subtotal = (float)subTotal,
                    Total = (float)subTotal + orderDetail.orderList.DeliveryFee,
                    UserId = user.Id,
                };
                dbContext.Add(orderList);
                dbContext.SaveChanges();

                foreach (SaleItem saleItem in orderDetail.saleItems)
                {
                    dbContext.Add(new SaleItem()
                    {
                        Quantity = saleItem.Quantity,
                        Discount = 0,
                        TasteId = saleItem.TasteId,
                        SizeId = saleItem.SizeId,
                        OrderListId = orderList.Id,
                        ProductId = saleItem.ProductId,
                    });

                    dbContext.SaveChanges();
                }

                _mailService.SendEmailToClientRegularAsync(orderDetail, orderList.Id, GetProducts());
                _mailService.SendInternalEmailRegularAsync(orderDetail, orderList.Id, GetProducts());
            }
        }

        /// <summary>
        /// To faciliate POST method CreateCustomOrder.
        /// </summary>
        /// <param name="customOrder">CustomOrder class contains all necessary objects to accommodate custom cake order in db.</param>
        public void CreateCustomOrder([FromBody] CustomOrder customOrder)
        {
            using (var dbContext = new BakingbunnyContext())
            {
                User user = new User()
                {
                    Firstname = customOrder.User.Firstname,
                    Lastname = customOrder.User.Lastname,
                    Email = customOrder.User.Email,
                    Phone = customOrder.User.Phone,
                    Address = customOrder.User.Address,
                    City = customOrder.User.City,
                    PostalCode = customOrder.User.PostalCode,
                };
                dbContext.Add(user);
                dbContext.SaveChanges();

                dbContext.Add(new CustomOrder()
                {
                    ExampleImage = customOrder.ExampleImage,
                    Message = customOrder.Message,
                    Comment = customOrder.Comment,
                    UserId = user.Id,
                    SizeId = customOrder.SizeId,
                    TasteId = customOrder.TasteId,
                    CakeTypeId = customOrder.CakeTypeId,
                });

                dbContext.SaveChanges();
            }

            _mailService.SendEmailToClientCustomAsync(customOrder);
            _mailService.SendInternalEmailCustomAsync(customOrder);
        }
    }
}
