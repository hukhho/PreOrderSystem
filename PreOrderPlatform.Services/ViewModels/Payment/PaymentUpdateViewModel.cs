using PreOrderPlatform.Entity.Enum.Payment;

namespace PreOrderPlatform.Service.ViewModels.Payment
{
    public class PaymentUpdateViewModel
    {
        public Guid Id { get; set; }
        public int? PaymentCount { get; set; }
        public decimal? PaymentAmount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
