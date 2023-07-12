using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.CampaignDetailServices
{
    public interface ICampaignDetailService
    {
        Task<CampaignPriceResponse> CreateCampaignDetailAsync(CampaignPriceCreateRequest model);
        Task DeleteCampaignDetailAsync(int id);
        Task<List<CampaignPriceResponse>> GetAllCampainDetailsWithProductAsync();
        Task<CampaignDetailByIdResponse> GetCampaignDetailByIdAsync(int id);
        Task<List<CampaignPriceResponse>> GetCampaignDetailsAsync();
        Task UpdateCampaignDetailAsync(CampaignPriceUpdateRequest model);
        Task<(IList<CampaignPriceResponse> campaigns, int totalItems)> GetAsync(PaginationParam<CampaignDetailEnum.CampaignDetailSort> paginationModel, CampaignDetailSearchRequest filterModel);
        
    }
}
