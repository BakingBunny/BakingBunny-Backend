using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Taste
    {
        public Taste()
        {
        }

        public int Id { get; set; }
        public string TasteName { get; set; }
    }
}
