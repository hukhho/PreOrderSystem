using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.Business.Request;
using PreOrderPlatform.Service.ViewModels.Business.Response;

namespace PreOrderPlatform.Service.Services.BusinessServices
{
    public interface IBusinessService
    {
        Task<BusinessResponse> CreateBusinessAsync(BusinessCreateRequest model);
        //Task DeleteBusinessAsync(Guid businessId);
        Task<BusinessByIdResponse> GetBusinessByIdAsync(Guid businessId);
        Task<List<BusinessResponse>> GetBusinessesAsync();
        Task UpdateBusinessAsync(BusinessUpdateRequest model);
        Task<(IList<BusinessResponse> businesses, int totalItems)> GetAsync(PaginationParam<BusinessEnum.BusinessSort> paginationModel, BusinessSearchRequest filterModel);
        Task<bool> CheckUserInBusiness(Guid userId, Guid businessId);
    }
}
