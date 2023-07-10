using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int? CampaignDetailId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? OrderId { get; set; }

        public virtual CampaignDetail? CampaignDetail { get; set; }
        public virtual Order? Order { get; set; }
    }
}
