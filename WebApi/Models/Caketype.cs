using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCrud.Models
{
    public partial class Caketype
    {
        public Caketype()
        {
            Customorders = new HashSet<Customorder>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Customorder> Customorders { get; set; }
    }
}
