using Newtonsoft.Json;

namespace PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential
{
    public class BusinessPaymentCredentialSearchRequest
    {
        public Guid? Id { get; set; }

        [JsonIgnore]
        public Guid? BusinessId { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankRecipientName { get; set; }
        public bool? IsMomoActive { get; set; }
        public bool? Status { get; set; }
    }
}
