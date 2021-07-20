using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Fruit
    {
        public Fruit()
        {
        }

        public int Id { get; set; }
        public string FruitName { get; set; }
    }
}
