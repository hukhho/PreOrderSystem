using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.OrderItem
{
    public class OrderItemSearchRequest
    {
        public int? CampaignDetailId { get; set; }
        public int? OrderId { get; set; }
    }
}
