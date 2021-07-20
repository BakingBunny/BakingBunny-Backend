using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly BakingbunnyContext _bakingbunnyContext;

        public ProductRepository(BakingbunnyContext bakingbunnyContext)
        {
            _bakingbunnyContext = bakingbunnyContext;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Product> GetAll()
        {
            return _bakingbunnyContext.Product.Where(s => s.Active == 1).ToList();
        }

        /// <summary>
        /// Retrieve all active cake products
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Product> GetCakes()
        {
            return _bakingbunnyContext.Product.Where(s => s.CategoryId == 1).Where(s => s.Active == 1).ToList();
        }

        /// <summary>
        /// Retrieve all active dacquoise products
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Product> GetDacquoises()
        {
            return _bakingbunnyContext.Product.Where(s => s.CategoryId == 2).Where(s => s.Active == 1).ToList();
        }

        /// <summary>
        /// Retrieve all sizes
        /// </summary>
        /// <returns>List<Size></returns>
        List<Size> IProductRepository.GetSizes()
        {
            return _bakingbunnyContext.Size.ToList();
        }

        /// <summary>
        /// Retrieve all fruits
        /// </summary>
        /// <returns>List<Fruit></returns>
        List<Fruit> IProductRepository.GetFruits()
        {
            return _bakingbunnyContext.Fruit.ToList();
        }

        /// <summary>
        /// Retrieve all cake types
        /// </summary>
        /// <returns>List<Caketypes></returns>
        List<Caketype> IProductRepository.GetCaketypes()
        {
            return _bakingbunnyContext.Caketype.ToList();
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
                        FruitId = saleItem.FruitId,
                        SizeId = saleItem.SizeId,
                        OrderListId = orderList.Id,
                        ProductId = saleItem.ProductId,
                    });

                    dbContext.SaveChanges();
                }
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
                    Name = customOrder.Name,
                    ExampleImage = customOrder.ExampleImage,
                    Message = customOrder.Message,
                    Comment = customOrder.Comment,
                    UserId = user.Id,
                    SizeId = customOrder.SizeId,
                    FruitId = customOrder.FruitId,
                    CakeTypeId = customOrder.CakeTypeId,
                });

                dbContext.SaveChanges();
            }
        }
    }
}
