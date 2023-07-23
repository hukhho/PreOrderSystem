namespace PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential
{
    public class BusinessPaymentCredentialViewModel
    {
        public Guid Id { get; set; }
        public Guid? BusinessId { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankRecipientName { get; set; }
        public string? MomoApiKey { get; set; }
        public string? MomoPartnerCode { get; set; }
        public string? MomoAccessToken { get; set; }
        public string? MomoSecretToken { get; set; }
        public bool? IsMomoActive { get; set; }
        public bool? IsMain { get; set; }
        public DateTime? CreateAt { get; set; }
        public bool? Status { get; set; }
    }
}
