using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCrud.Models
{
    public partial class Size
    {
        public Size()
        {
            Customorders = new HashSet<Customorder>();
            Saleitems = new HashSet<Saleitem>();
        }

        public int Id { get; set; }
        public string Size1 { get; set; }

        public virtual ICollection<Customorder> Customorders { get; set; }
        public virtual ICollection<Saleitem> Saleitems { get; set; }
    }
}
