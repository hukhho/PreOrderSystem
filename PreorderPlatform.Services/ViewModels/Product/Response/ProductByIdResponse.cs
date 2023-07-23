using PreOrderPlatform.Service.ViewModels.Business.Response;
using PreOrderPlatform.Service.ViewModels.Campaign.Response;
using PreOrderPlatform.Service.ViewModels.Category.Response;

namespace PreOrderPlatform.Service.ViewModels.Product.Response
{
    public class ProductByIdResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Status { get; set; }
        public Guid? BusinessId { get; set; }

        public virtual BusinessResponse? Business { get; set; }
        public virtual CategoryResponse? Category { get; set; }
        public virtual ICollection<CampaignResponse> Campaigns { get; set; }
    }
}
