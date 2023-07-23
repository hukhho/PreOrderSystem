using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PreOrderPlatform.Entity.Enum.Campaign;
using PreOrderPlatform.Entity.Enum.Visibility;
using PreOrderPlatform.Service.ViewModels.Campaign.CampaignImages;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;

namespace PreOrderPlatform.Service.ViewModels.Campaign.Request
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

        [Required]
        public Guid ProductId { get; set; }

        [JsonIgnore]
        public Guid? OwnerId { get; set; }

        [JsonIgnore]
        public Guid? BusinessId { get; set; }

        public List<CampaignImageCreate>? Images { get; set; }

        public List<CampaignPriceCreateRequest>? CampaignDetails { get; set; }

    }
}
