using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential.Response;
using PreOrderPlatform.Service.ViewModels.Campaign.Response;
using PreOrderPlatform.Service.ViewModels.Product.Response;
using PreOrderPlatform.Service.ViewModels.User.Response;

namespace PreOrderPlatform.Service.ViewModels.Business.Response
{
    public class BusinessByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? LogoUrl { get; set; }
        public Guid OwnerId { get; set; }
        public bool IsVerified { get; set; }
        public bool Status { get; set; }

        public virtual UserResponse? Owner { get; set; }
        public virtual ICollection<BusinessPaymentCredentialResponse> BusinessPaymentCredentials { get; set; }
        public virtual ICollection<CampaignResponse> Campaigns { get; set; }
        public virtual ICollection<ProductResponse> Products { get; set; }
        public virtual ICollection<UserResponse> Users { get; set; }
    }
}
