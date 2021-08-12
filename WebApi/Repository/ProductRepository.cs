using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IHttpClientFactory _clientFactory;

        public ProductRepository(BakingbunnyContext bakingbunnyContext, IMailService mailService, IHttpClientFactory clientFactory)
        {
            _bakingbunnyContext = bakingbunnyContext;
            _mailService = mailService;
            _clientFactory = clientFactory;
        }

        public ProductDetail GetProductById(int Id)
        {
            ProductDetail productDetail = new ProductDetail();
            Product product = GetById(Id);
            List<Taste> tasteList = GetTastes();
            List<Size> sizeList = GetSizes();

            productDetail.ProductId = product.Id;
            productDetail.ProductName = product.Name;
            productDetail.Price = product.Price;
            productDetail.Description = product.Description;
            productDetail.ProductImage = product.ProductImage;
            productDetail.Comment = product.Comment;
            productDetail.CategoryId = product.CategoryId;

            // Taste
            if (product.Id == 29)
                productDetail.TasteList = tasteList.Where(t => t.Id >= 4 && t.Id <= 9).ToList();
            else if (product.Id == 2)
                productDetail.TasteList = tasteList.Where(t => t.Id <= 3).ToList();
            else
                productDetail.TasteList = new List<Taste>();

            // Size
            if (product.CategoryId == 1)
                productDetail.SizeList = sizeList;
            else
                productDetail.SizeList = new List<Size>();

            return productDetail;
        }

        /// <summary>
        /// Get all product details for frontend
        /// </summary>
        /// <returns>List<ProductDetail></returns>
        public List<ProductDetail> GetAll()
        {
            List<ProductDetail> productDetailList = new List<ProductDetail>();
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
        /// Retrieve only cake list
        /// </summary>
        /// <returns>List<ProductDetail></returns>
        public List<ProductDetail> GetCakes()
        {
            List<ProductDetail> productDetailList = new List<ProductDetail>();
            List<Product> productList = GetProducts().Where(p => p.CategoryId == 1).ToList();
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
                productDetail.SizeList = sizeList;

                productDetailList.Add(productDetail);
            }


            return productDetailList;
        }

        /// <summary>
        /// Retrieve only dacquoise list
        /// </summary>
        /// <returns>List<ProductDetail></returns>
        public List<ProductDetail> GetDacquoises()
        {
            List<ProductDetail> productDetailList = new List<ProductDetail>();
            List<Product> productList = GetProducts().Where(p => p.CategoryId == 2).ToList(); ;
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
        /// Retrieve a Product object by Id
        /// </summary>
        /// <returns>Product</returns>
        private Product GetById(int Id)
        {
            return _bakingbunnyContext.Product.Where(s => s.Active).Where(p => p.Id == Id).FirstOrDefault();
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

                string postalCode = orderDetail.user.PostalCode.ToLower();
                postalCode = postalCode.Replace(" ", "");
                Delivery delivery = FindDeliveryFee(postalCode);
                int deliveryFee = delivery == null ? 0 : delivery.DeliveryFee;
                orderDetail.orderList.DeliveryFee = deliveryFee;

                OrderList orderList = new OrderList()
                {
                    PickupDeliveryDate = orderDetail.orderList.PickupDeliveryDate,
                    Delivery = orderDetail.orderList.Delivery,
                    DeliveryFee = deliveryFee,
                    OrderDate = DateTime.Now,
                    Subtotal = (float)subTotal,
                    Total = (float)subTotal + deliveryFee,
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
                    RequestDescription = customOrder.RequestDescription,
                    RequestDate = customOrder.RequestDate,
                    Delivery = customOrder.Delivery,
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

        public int CalculateDeliveryFee(string postalCode)
        {
            int distance = -1;
            int deliveryFee = -1;

            postalCode = postalCode.Replace(" ", "");
            postalCode = postalCode.ToLower();

            Delivery delivery = FindDeliveryFee(postalCode);

            if (delivery != null)
            {
                return delivery.DeliveryFee;
            }
            else
            {
                distance = GetDistance(postalCode);

                if (distance < 0)
                    deliveryFee = -1;
                else if (distance < 5000)
                    deliveryFee = 0;
                else if (distance < 10000)
                    deliveryFee = 3;
                else if (distance < 15000)
                    deliveryFee = 5;
                else if (distance < 20000)
                    deliveryFee = 7;
                else if (distance < 25000)
                    deliveryFee = 10;
                else
                    deliveryFee = -1;

                if (deliveryFee > -1)
                {
                    AddDeliveryInfo(new Delivery()
                    {
                        PostalCode = postalCode,
                        DeliveryFee = deliveryFee,
                        Distance = distance
                    });
                }
            }

            return deliveryFee;
        }

        private int GetDistance (string postalCode)
        {
#if (DEBUG)
            //postalCode = "T2Y3E4";
            //postalCode = "T3M3A7";
#endif

            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=T2P3P3&destinations=" + postalCode + "&departure_time=now&key=AIzaSyCq7_wnyksRIYf6kOhCQ555TDZT0TKoeQY";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var client = _clientFactory.CreateClient();
            var response = client.Send(request);

            int distance = 0; // Distance in meters

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = response.Content.ReadAsStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string jsonResponse = reader.ReadToEnd();
                        JToken destination = JObject.Parse(jsonResponse) ["destination_addresses"] [0];
                        
                        //if (!destination.HasValues)
                        if (destination == null)
                            return -1;

                        JToken jToken = JObject.Parse(jsonResponse) ["rows"] [0] ["elements"] [0] ["distance"];
                        distance = jToken ["value"].Value<int>();
                    }
                }
            }
            return distance;
        }

        private Delivery FindDeliveryFee (string postalCode)
        {
            return _bakingbunnyContext.Delivery.Where(d => d.PostalCode.Equals(postalCode)).FirstOrDefault();
        }

        private void AddDeliveryInfo(Delivery delivery)
        {
            using (var dbContext = new BakingbunnyContext())
            {
                dbContext.Add(delivery);
                dbContext.SaveChanges();
            }
        }
    }
}
