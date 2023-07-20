using PreorderPlatform.Entity.Repositories.Enum.Campaign;
using PreorderPlatform.Entity.Repositories.Enum.Status;
using PreorderPlatform.Entity.Repositories.Enum.Visibility;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Response;
using PreorderPlatform.Service.ViewModels.Product.Response;
using PreorderPlatform.Service.ViewModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Campaign.Response
{
    public class CampaignDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int DepositPercent { get; set; }
        public DateTime? ExpectedShippingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CampaignType Type { get; set; }
        public CampaignLocation Location { get; set; }
        public CampaignStatus Status { get; set; }
        public Visibility Visibility { get; set; }
        public bool IsDeleted { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BusinessId { get; set; }

        public virtual ProductResponse? Product { get; set; }
        public virtual BusinessResponse? Business { get; set; }
        public virtual UserResponse? Owner { get; set; }
        public virtual ICollection<CampaignPriceResponse>? CampaignDetails { get; set; }
    }
}
