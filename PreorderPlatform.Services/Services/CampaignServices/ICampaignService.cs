using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.Campaign.Request;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.CampaignServices
{
    public interface ICampaignService
    {
        Task<CampaignResponse> CreateCampaignAsync(CampaignCreateRequest model);
        Task DeleteCampaignAsync(int id);
        Task<List<CampaignResponse>> GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync();
        Task<CampaignResponse> GetCampaignByIdAsync(int id);
        Task<List<CampaignResponse>> GetCampaignsAsync();
        Task UpdateCampaignAsync(CampaignUpdateRequest model);
        //Get all campaign/filtered with sort&pagiantion
    
        Task<(IList<CampaignResponse> campaigns, int totalItems)> GetAsync(PaginationParam<CampaignEnum.CampaignSort> paginationModel, CampaignSearchRequest filterModel);
    }
}
