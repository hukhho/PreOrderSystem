using PreOrderPlatform.Entity.Enum.Payment;

namespace PreOrderPlatform.Entity.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Id = Guid.NewGuid();
            DateTime now = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).DateTime;
            CreatedAt = now;
        }
        public Guid Id { get; set; }
        public int? PaymentCount { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string? TransactionId { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime? PayedAt { get; set; }
        public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual User? User { get; set; }
    }
}
