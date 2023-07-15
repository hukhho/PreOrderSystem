using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.OrderItem
{
    public class OrderItemUpdateViewModel
    {
        public Guid Id { get; set; }
        public Guid? CampaignDetailId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? OrderId { get; set; }
    }
}
