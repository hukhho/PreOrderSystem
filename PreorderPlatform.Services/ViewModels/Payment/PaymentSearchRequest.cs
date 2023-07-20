using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Payment
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
