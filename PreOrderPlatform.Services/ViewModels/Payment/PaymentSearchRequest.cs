using Newtonsoft.Json;

namespace PreOrderPlatform.Service.ViewModels.Payment
{
    public class PaymentSearchRequest
    {
        public Guid Id { get; set; }
        public string? Method { get; set; }
        public decimal? Total { get; set; }
        public int? PaymentCount { get; set; }
        public DateTime? PayedAt { get; set; }
        public string? Status { get; set; }

        [JsonIgnore]
        public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }
    }
}
