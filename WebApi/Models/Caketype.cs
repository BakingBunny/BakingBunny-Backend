using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class CakeType
    {
        public CakeType()
        {
        }

        public int Id { get; set; }
        public string Type { get; set; }
    }
}
