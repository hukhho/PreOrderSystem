using PreorderPlatform.Service.ViewModels.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.BusinessPaymentCredential.Response
{
    public class BusinessPaymentByIdResponse
    {
        public int Id { get; set; }
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

        public virtual BusinessResponse? Business { get; set; }
    }
}
