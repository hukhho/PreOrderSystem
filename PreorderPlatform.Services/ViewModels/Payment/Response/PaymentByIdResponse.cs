using PreorderPlatform.Service.ViewModels.Order.Response;
using PreorderPlatform.Service.ViewModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Payment.Response
{
    public class PaymentByIdResponse
    {
        public int Id { get; set; }
        public string? Method { get; set; }
        public decimal? Total { get; set; }
        public int? PaymentCount { get; set; }
        public DateTime? PayedAt { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
        public int? OrderId { get; set; }

        public virtual OrderResponse? Order { get; set; }
        public virtual UserResponse? User { get; set; }
    }
}
