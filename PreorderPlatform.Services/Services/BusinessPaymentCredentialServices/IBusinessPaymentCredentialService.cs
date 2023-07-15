using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.BusinessPaymentCredential;
using PreorderPlatform.Services.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.BusinessPaymentCredentialServices
{
    public interface IBusinessPaymentCredentialService
    {
        Task<BusinessPaymentCredentialViewModel> CreateBusinessPaymentCredentialAsync(BusinessPaymentCredentialCreateViewModel model);
        Task DeleteBusinessPaymentCredentialAsync(Guid id);
        Task<BusinessPaymentCredentialViewModel> GetBusinessPaymentCredentialByIdAsync(Guid id, string? userId);
        Task<List<BusinessPaymentCredentialViewModel>> GetBusinessPaymentCredentialsAsync();
        Task UpdateBusinessPaymentCredentialAsync(BusinessPaymentCredentialUpdateViewModel model);
        Task<(IList<BusinessPaymentCredentialViewModel> businessPaymentCredentials, int totalItems)> GetAsync(PaginationParam<BusinessPaymentCredentialEnum.BusinessPaymentCredentialSort> paginationModel, BusinessPaymentCredentialSearchRequest filterModel);
        Task<Business> GetBusinessByOwnerIdAsync(Guid userId);
    }
}
