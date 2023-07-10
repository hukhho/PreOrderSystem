using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.CampaignPrice.Request
{
    public class CampaignPriceCreateRequest
    {
        public int? ProductId { get; set; }
        public int? MinOrder { get; set; }
        public int? MaxOrder { get; set; }
        public int? CampaignId { get; set; }
        public decimal? Price { get; set; }
    }
}
