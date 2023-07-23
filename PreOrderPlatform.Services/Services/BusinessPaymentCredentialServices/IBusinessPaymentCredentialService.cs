using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential;

namespace PreOrderPlatform.Service.Services.BusinessPaymentCredentialServices
{
    public interface IBusinessPaymentCredentialService
    {
        Task<BusinessPaymentCredentialViewModel> CreateBusinessPaymentCredentialAsync(BusinessPaymentCredentialCreateViewModel model);
        Task DeleteBusinessPaymentCredentialAsync(Guid id);
        Task<BusinessPaymentCredentialViewModel> GetBusinessPaymentCredentialByIdAsync(Guid id);
        Task<List<BusinessPaymentCredentialViewModel>> GetBusinessPaymentCredentialsAsync();
        Task UpdateBusinessPaymentCredentialAsync(Guid id, BusinessPaymentCredentialUpdateViewModel model);
        Task<(IList<BusinessPaymentCredentialViewModel> businessPaymentCredentials, int totalItems)> GetAsync(PaginationParam<BusinessPaymentCredentialEnum.BusinessPaymentCredentialSort> paginationModel, BusinessPaymentCredentialSearchRequest filterModel);
        Task<Business> GetBusinessByOwnerIdAsync(Guid userId);
    }
}
