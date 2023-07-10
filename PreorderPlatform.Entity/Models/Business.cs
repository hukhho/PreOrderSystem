using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Business
    {
        public Business()
        {
            BusinessPaymentCredentials = new HashSet<BusinessPaymentCredential>();
            Campaigns = new HashSet<Campaign>();
            Products = new HashSet<Product>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? OwnerId { get; set; }
        public bool? Status { get; set; }

        public virtual User? Owner { get; set; }
        public virtual ICollection<BusinessPaymentCredential> BusinessPaymentCredentials { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
