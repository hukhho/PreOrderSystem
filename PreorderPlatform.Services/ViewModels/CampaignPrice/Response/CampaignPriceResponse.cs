using PreorderPlatform.Service.ViewModels.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.CampaignPrice.Response
{
    public class CampaignPriceResponse
    {
        public Guid Id { get; set; }
        public int? Phase { get; set; }
        public int? AllowedQuantity { get; set; }
        public int? TotalOrdered { get; set; }
        public Guid? CampaignId { get; set; }
        public decimal? Price { get; set; }
    }
}
