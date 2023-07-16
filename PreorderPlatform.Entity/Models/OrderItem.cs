using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class OrderItem
    {
        public OrderItem()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid CampaignDetailId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }

        public virtual CampaignDetail CampaignDetail { get; set; }
        public virtual Order Order { get; set; }
    }
}
