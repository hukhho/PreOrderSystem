using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.CampaignPrice.Request
{
    public class CampaignPriceCreateRequest
    {
        // Request properties
        public int? Phase { get; set; }
        public int? AllowedQuantity { get; set; }
        public int? TotalOrdered { get; set; }
        public Guid? CampaignId { get; set; }
        public decimal? Price { get; set; }
    }
}
