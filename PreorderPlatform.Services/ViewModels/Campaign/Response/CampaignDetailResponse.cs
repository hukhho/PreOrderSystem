using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Response;
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
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? DepositPercent { get; set; }
        public DateTime? ExpectedShippingDate { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool? Status { get; set; }

        public virtual BusinessResponse? Business { get; set; }
        public virtual UserResponse? Owner { get; set; }
        public virtual ICollection<CampaignPriceResponse>? CampaignDetails { get; set; }
    }
}
