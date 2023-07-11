using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.BusinessPaymentCredential
{
    public class BusinessPaymentCredentialSearchRequest
    {
        public int? Id { get; set; }
        public int? BusinessId { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankRecipientName { get; set; }
        public bool? IsMomoActive { get; set; }
        public DateTime? CreateAt { get; set; }
        public bool? Status { get; set; }
    }
}
