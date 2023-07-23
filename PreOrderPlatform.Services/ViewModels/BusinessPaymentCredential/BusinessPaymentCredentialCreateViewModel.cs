using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential
{
    public class BusinessPaymentCredentialCreateViewModel
    {

        [StringLength(30, MinimumLength = 6)]
        public string? BankAccountNumber { get; set; }

        [StringLength(30, MinimumLength = 2)]
        public string? BankName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string? BankRecipientName { get; set; }

        [StringLength(200)]
        public string? MomoApiKey { get; set; }

        [StringLength(200)]
        public string? MomoPartnerCode { get; set; }

        [StringLength(200)]
        public string? MomoAccessToken { get; set; }

        [Required]
        public string? MomoSecretToken { get; set; }

        [Required]
        public bool IsMomoActive { get; set; }

        [JsonIgnore]
        public Guid BusinessId { get; set; }

        [JsonIgnore]
        public DateTime? CreateAt { get; set; }

        [JsonIgnore]
        public bool IsMain { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }
    }
}
