using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.Order.Response;
using PreorderPlatform.Service.ViewModels.Payment.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.User.Response
{
    public class UserByIdResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public bool? Status { get; set; }
        public string? RoleName { get; set; }

        public virtual BusinessResponse? Business { get; set; }
        public virtual ICollection<CampaignResponse> Campaigns { get; set; }
        public virtual ICollection<OrderResponse> Orders { get; set; }
        public virtual ICollection<PaymentResponse> Payments { get; set; }
    }
}
