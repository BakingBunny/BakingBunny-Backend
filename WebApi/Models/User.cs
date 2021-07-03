using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiCrud.Models
{
    public partial class User
    {
        public User()
        {
            Customorders = new HashSet<Customorder>();
            Orderlists = new HashSet<Orderlist>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }

        public virtual ICollection<Customorder> Customorders { get; set; }
        public virtual ICollection<Orderlist> Orderlists { get; set; }
    }
}
