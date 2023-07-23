using PreOrderPlatform.Service.ViewModels.Business.Response;
using PreOrderPlatform.Service.ViewModels.Campaign.CampaignImages;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Response;
using PreOrderPlatform.Service.ViewModels.Product.Response;

namespace PreOrderPlatform.Service.ViewModels.Campaign.Response
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
        public string Type { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Visibility { get; set; }
        public bool IsDeleted { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BusinessId { get; set; }

        public List<CampaignPriceResponse> CampaignDetail { get; set; }
        public List<CampaignImageView> CampaignImages { get; set; }


        public ProductResponse Product { get; set; }
        public BusinessResponse Business { get; set; }

         
        //public virtual ProductResponse? Product { get; set; }
        //public virtual BusinessResponse? Business { get; set; }
        //public virtual UserResponse? Owner { get; set; }
        //public virtual ICollection<CampaignImage> Images { get; set; }
        //public virtual ICollection<CampaignDetail> CampaignDetails { get; set; }
    }
}
