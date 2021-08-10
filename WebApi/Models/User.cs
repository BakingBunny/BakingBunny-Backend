using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApi.Models
{
    public partial class User
    {
        public User()
        {
        }

        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        [Required]
        public string Phone { get; set; }
        public string City { get; set; }
    }
}
