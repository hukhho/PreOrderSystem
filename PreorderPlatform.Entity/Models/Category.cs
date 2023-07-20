using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Category
    {
        public Category()
        {
            Id = Guid.NewGuid();
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
