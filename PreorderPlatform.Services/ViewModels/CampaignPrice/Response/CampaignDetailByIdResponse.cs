using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.OrderItem.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.CampaignPrice.Response
{
    public class CampaignDetailByIdResponse
    {
        public int Id { get; set; }
        public int? Phase { get; set; }
        public int? AllowedQuantity { get; set; }
        public int? TotalOrdered { get; set; }
        public decimal? Price { get; set; }

        public virtual CampaignResponse? Campaign { get; set; }
        public virtual ICollection<OrderItemResponse> OrderItems { get; set; }
    }
}
