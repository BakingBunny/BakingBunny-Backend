using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Size
    {
        public Size()
        {
        }

        public int Id { get; set; }
        public string SizeName { get; set; }
    }
}
