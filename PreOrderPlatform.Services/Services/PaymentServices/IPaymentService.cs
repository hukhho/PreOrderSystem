using Newtonsoft.Json.Linq;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.Payment;

namespace PreOrderPlatform.Service.Services.PaymentServices
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
        Task<JObject> CreateMomoPayment(MomoPaymentCreateViewModel model);
    }
}
