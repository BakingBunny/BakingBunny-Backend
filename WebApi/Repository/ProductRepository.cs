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
        public Product GetProductById(int id)
        {
            return _bakingbunnyContext.Product.Where(s => s.Id == id).FirstOrDefault<Product>();
        }

        public List<Product> GetAll()
        {
            return _bakingbunnyContext.Product.ToList();
        }

        public List<Product> GetCakes()
        {
            return _bakingbunnyContext.Product.Where(s => s.CategoryId == 1).ToList();
        }

        public List<Product> GetDacquoises()
        {
            return _bakingbunnyContext.Product.Where(s => s.CategoryId == 2).ToList();
        }

        public List<Product> GetDacquoisesCombo()
        {
            int [] dacquoises = new int [] { 11, 13, 18, 23, 24, 29 };
            return _bakingbunnyContext.Product.Where(s => s.CategoryId == 2).Where(s => dacquoises.Contains(s.Id)).ToList();
        }

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
                foreach (Product p in orderDetail.productList)
                    subTotal += p.Price;

                Orderlist orderList = new Orderlist()
                {
                    Delivery = orderDetail.orderlist.Delivery,
                    DeliveryFee = orderDetail.orderlist.DeliveryFee,
                    OrderDate = DateTime.Now,
                    Subtotal = (float)subTotal,
                    Total = (float)subTotal + orderDetail.orderlist.DeliveryFee,
                    UserId = user.Id,
                };
                dbContext.Add(orderList);
                dbContext.SaveChanges();

                Fruit chosenFruit = _bakingbunnyContext.Fruit.Where(s => s.FruitName.Equals(orderDetail.fruit.FruitName)).FirstOrDefault();
                Size chosenSize = _bakingbunnyContext.Size.Where(s => s.SizeName.Equals(orderDetail.size.SizeName)).FirstOrDefault();

                foreach (Product product in orderDetail.productList)
                {
                    dbContext.Add(new Saleitem()
                    {
                        Quantity = orderDetail.quantity,
                        Discount = 0, // Maybe remove this?
                        FruitId = chosenFruit.Id,
                        SizeId = chosenSize.Id,
                        OrderListId = orderList.Id,
                        ProductId = product.Id,
                    });
                }

                dbContext.SaveChanges();

            }
        }

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

                Fruit chosenFruit = _bakingbunnyContext.Fruit.Where(s => s.FruitName.Equals(customOrder.Fruit.FruitName)).FirstOrDefault();
                Size chosenSize = _bakingbunnyContext.Size.Where(s => s.SizeName.Equals(customOrder.Size.SizeName)).FirstOrDefault();
                Caketype chosenCakeType = _bakingbunnyContext.Caketype.Where(s => s.Type.Equals(customOrder.CakeType.Type)).FirstOrDefault();

                dbContext.Add(new CustomOrder()
                {
                    Name = customOrder.Name,
                    ExampleImage = customOrder.ExampleImage,
                    Message = customOrder.Message,
                    Comment = customOrder.Comment,
                    UserId = user.Id,
                    SizeId = chosenSize.Id,
                    FruitId = chosenFruit.Id,
                    CakeTypeId = chosenCakeType.Id,
                });

                dbContext.SaveChanges();
            }
        }
    }
}
