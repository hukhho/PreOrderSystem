using PreOrderPlatform.Entity.Enum.Campaign;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.Campaign.Request;
using PreOrderPlatform.Service.ViewModels.Campaign.Response;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;

namespace PreOrderPlatform.Service.Services.CampaignServices
{
    public interface ICampaignService
    {
        Task<CampaignDetailResponse> CreateCampaignAsync(CampaignCreateRequest model);
        Task DeleteCampaignAsync(Guid id);
        //Task<List<CampaignResponse>> GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync();
        Task<CampaignDetailResponse> GetCampaignByIdAsync(Guid id);
        Task<List<CampaignResponse>> GetCampaignsAsync();
        Task UpdateCampaignAsync(CampaignUpdateRequest model);
        //Get all campaign/filtered with sort&pagiantion
    
        Task<(IList<CampaignResponse> campaigns, int totalItems)> GetAsync(PaginationParam<CampaignEnum.CampaignSort> paginationModel, CampaignSearchRequest filterModel);
        Task ChangeCampaignStatusAsync(Guid campaignId, CampaignStatus newStatus);
        Task UpdateCampaignDetailsAsync(Guid campaignId, List<CampaignPriceUpdateRequest> newDetails);
        Task DeleteCampaignDetailAsync(Guid campaignId, int phase);
    }
}
