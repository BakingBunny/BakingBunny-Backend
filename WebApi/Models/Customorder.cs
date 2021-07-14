using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Customorder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExampleImage { get; set; }
        public string Message { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int SizeId { get; set; }
        public int FruitId { get; set; }
        public int CakeTypeId { get; set; }

        public virtual Caketype CakeType { get; set; }
        public virtual Fruit Fruit { get; set; }
        public virtual Size Size { get; set; }
        public virtual User User { get; set; }
    }
}
