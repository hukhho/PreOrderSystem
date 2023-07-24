using PreOrderPlatform.Entity.Enum.Payment;

namespace PreOrderPlatform.Service.ViewModels.Payment
{
    public class MomoPaymentCreateViewModel
    {
        public string partnerCode { get; set; }
        public string accessKey { get; set; }
        public string serectkey { get; set; }
        public string orderInfo { get; set; }
        public string redirectUrl  { get; set; }
        public string inputUrl { get; set; }

        public string requestType { get; set; }

        public string amount { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
    }
}
