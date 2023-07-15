using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class CampaignDetail
    {
        public CampaignDetail()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public Guid Id { get; set; }
        public int? Phase { get; set; }
        public int? AllowedQuantity { get; set; }
        public int? TotalOrdered { get; set; }
        public Guid? CampaignId { get; set; }
        public decimal? Price { get; set; }

        public virtual Campaign? Campaign { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
