using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.Order.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.OrderItem.Response
{
    public class OrderItemByIdResponse
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual CampaignDetailResponse? CampaignDetail { get; set; }
        public virtual OrderResponse? Order { get; set; }
    }
}
