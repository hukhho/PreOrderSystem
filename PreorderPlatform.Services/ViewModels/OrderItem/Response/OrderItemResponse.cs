using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.OrderItem.Response
{
    public class OrderItemResponse
    {
        public Guid Id { get; set; }
        public Guid? CampaignDetailId { get; set; }
        public Guid? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? OrderId { get; set; }
    }
}
