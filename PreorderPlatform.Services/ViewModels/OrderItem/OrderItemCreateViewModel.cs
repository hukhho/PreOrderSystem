using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.OrderItem
{
    public class OrderItemCreateViewModel
    {
        public Guid? CampaignDetailId { get; set; }
        public Guid? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? OrderId { get; set; }
    }
}
