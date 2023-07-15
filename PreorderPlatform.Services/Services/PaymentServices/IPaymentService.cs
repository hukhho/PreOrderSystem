using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.Payment;

namespace PreorderPlatform.Service.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<PaymentViewModel> CreatePaymentAsync(PaymentCreateViewModel model);

        Task DeletePaymentAsync(Guid id);

        Task<PaymentViewModel> GetPaymentByIdAsync(Guid id);

        Task<List<PaymentViewModel>> GetPaymentsAsync();

        Task UpdatePaymentAsync(PaymentUpdateViewModel model);

        Task<(IList<PaymentViewModel> payments, int totalItems)>
        GetAsync(

                PaginationParam<PaymentEnum.PaymentSort> paginationModel,
                PaymentSearchRequest filterModel

        );

        Task<JObject> TestMomo();

        Task<string> TestVNPay();
    }
}
