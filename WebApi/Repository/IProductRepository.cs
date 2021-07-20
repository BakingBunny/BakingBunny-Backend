﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        List<Product> GetCakes();
        List<Product> GetDacquoises();
        List<Size> GetSizes();
        List<Fruit> GetFruits();
        List<Caketype> GetCaketypes();
        void CreateOrder([FromBody] OrderDetail orderDetail);

        void CreateCustomOrder([FromBody] CustomOrder customOrder);
    }
}
