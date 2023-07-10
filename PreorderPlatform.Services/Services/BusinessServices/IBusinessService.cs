using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.BusinessServices
{
    public interface IBusinessService
    {
        Task<BusinessResponse> CreateBusinessAsync(BusinessCreateRequest model);
        Task DeleteBusinessAsync(int id);
        Task<BusinessResponse> GetBusinessByIdAsync(int id);
        Task<List<BusinessResponse>> GetBusinessesAsync();
        Task UpdateBusinessAsync(BusinessUpdateRequest model);
    }
}
