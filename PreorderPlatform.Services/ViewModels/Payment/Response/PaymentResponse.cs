using PreOrderPlatform.Entity.Enum.Payment;

namespace PreOrderPlatform.Service.ViewModels.Payment.Response
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public int? PaymentCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? PaymentAmount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime? PayedAt { get; set; }
        public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }
    }
}
