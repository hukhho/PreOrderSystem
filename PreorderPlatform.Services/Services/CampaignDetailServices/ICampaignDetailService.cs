using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Response;

namespace PreOrderPlatform.Service.Services.CampaignDetailServices
{
    public interface ICampaignDetailService
    {
        Task<CampaignPriceResponse> CreateCampaignDetailAsync(CampaignPriceCreateRequest model);
        Task DeleteCampaignDetailAsync(Guid id);
        Task<List<CampaignPriceResponse>> GetAllCampainDetailsWithProductAsync();
        Task<CampaignPriceResponse> GetCampaignDetailByIdAsync(Guid id);
        Task<List<CampaignPriceResponse>> GetCampaignDetailsAsync();
        Task UpdateCampaignDetailAsync(CampaignPriceUpdateRequest model);
        Task<(IList<CampaignPriceResponse> campaigns, int totalItems)> GetAsync(PaginationParam<CampaignDetailEnum.CampaignDetailSort> paginationModel, CampaignDetailSearchRequest filterModel);
        
    }
}
