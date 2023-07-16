using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Product
    {
        public Product()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BusinessId { get; set; }

        public virtual Business Business { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
