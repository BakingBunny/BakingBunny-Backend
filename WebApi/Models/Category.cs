﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Category
    {
        public Category()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
