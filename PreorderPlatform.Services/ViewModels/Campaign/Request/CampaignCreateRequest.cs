using Newtonsoft.Json;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.Enum.Campaign;
using PreorderPlatform.Entity.Repositories.Enum.Status;
using PreorderPlatform.Entity.Repositories.Enum.Visibility;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreorderPlatform.Service.ViewModels.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Campaign.Request
{
    public class CampaignCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int DepositPercent { get; set; }
        public DateTime? ExpectedShippingDate { get; set; }
        public CampaignType Type { get; set; }
        public CampaignLocation Location { get; set; }
        public Visibility Visibility { get; set; }

        [JsonRequired]
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Guid OwnerId { get; set; }
        [JsonIgnore]
        public Guid BusinessId { get; set; }

        public List<CampaignImage>? Images { get; set; }

        public List<CampaignPriceCreateRequest>? CampaignPriceCreates { get; set; }

    }
}
