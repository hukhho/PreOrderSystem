using PreorderPlatform.Service.ViewModels.BusinessPaymentCredential;
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
        Task DeleteBusinessPaymentCredentialAsync(int id);
        Task<BusinessPaymentCredentialViewModel> GetBusinessPaymentCredentialByIdAsync(int id);
        Task<List<BusinessPaymentCredentialViewModel>> GetBusinessPaymentCredentialsAsync();
        Task UpdateBusinessPaymentCredentialAsync(BusinessPaymentCredentialUpdateViewModel model);
    }
}
