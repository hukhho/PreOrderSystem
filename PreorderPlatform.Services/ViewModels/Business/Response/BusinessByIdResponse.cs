using PreorderPlatform.Service.ViewModels.BusinessPaymentCredential.Response;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.Product.Response;
using PreorderPlatform.Service.ViewModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Business.Response
{
    public class BusinessByIdResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }

        public virtual UserResponse? Owner { get; set; }
        public virtual ICollection<BusinessPaymentCredentialResponse> BusinessPaymentCredentials { get; set; }
        public virtual ICollection<CampaignResponse> Campaigns { get; set; }
        public virtual ICollection<ProductResponse> Products { get; set; }
        public virtual ICollection<UserResponse> Users { get; set; }
    }
}
