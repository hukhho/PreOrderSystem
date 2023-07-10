using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class User
    {
        public User()
        {
            Businesses = new HashSet<Business>();
            Campaigns = new HashSet<Campaign>();
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public bool? Status { get; set; }
        public int? RoleId { get; set; }
        public int? BusinessId { get; set; }

        public virtual Business? Business { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
