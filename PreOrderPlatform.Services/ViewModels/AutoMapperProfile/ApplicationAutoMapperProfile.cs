using AutoMapper;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.ViewModels.Business.Request;
using PreOrderPlatform.Service.ViewModels.Business.Response;
using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential;
using PreOrderPlatform.Service.ViewModels.Campaign.Request;
using PreOrderPlatform.Service.ViewModels.Campaign.Response;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Response;
using PreOrderPlatform.Service.ViewModels.Category;
using PreOrderPlatform.Service.ViewModels.Order;
using PreOrderPlatform.Service.ViewModels.OrderItem;
using PreOrderPlatform.Service.ViewModels.Payment;
using PreOrderPlatform.Service.ViewModels.Product.Request;
using PreOrderPlatform.Service.ViewModels.Product.Response;
using PreOrderPlatform.Service.ViewModels.Role;
using PreOrderPlatform.Service.ViewModels.User.Request;
using PreOrderPlatform.Service.ViewModels.User.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            // User mappings
            CreateMap<Entity.Models.User, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business.Name))
                .ReverseMap();
            CreateMap<Entity.Models.User, UserUpdateRequest>().ReverseMap();
            CreateMap<Entity.Models.User, UserCreateRequest>().ReverseMap();

            //Role mappings
            CreateMap<Entity.Models.Role, RoleCreateViewModel>().ReverseMap();
            CreateMap<Entity.Models.Role, RoleDetailViewModel>().ReverseMap();

            //Category
            CreateMap<Entity.Models.Category, CategoryCreateViewModel>().ReverseMap();
            CreateMap<Entity.Models.Category, CategoryUpdateViewModel>().ReverseMap();
            CreateMap<Entity.Models.Category, CategoryViewModel>().ReverseMap();

            //Product
            CreateMap<Entity.Models.Product, ProductCreateRequest>().ReverseMap();
            CreateMap<Entity.Models.Product, ProductUpdateRequest>().ReverseMap();
            CreateMap<Entity.Models.Product, ProductResponse>()
                                                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                                .ReverseMap();

            //Business
            CreateMap<Entity.Models.Business, BusinessCreateRequest>().ReverseMap();
            CreateMap<Entity.Models.Business, BusinessUpdateRequest>().ReverseMap();
            CreateMap<Entity.Models.Business, BusinessResponse>().ReverseMap();

            //BussinessPaymentCrential
            CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialCreateViewModel>().ReverseMap();
            CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialUpdateViewModel>().ReverseMap();
            CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialViewModel>().ReverseMap();

            //Campaign
            CreateMap<Entity.Models.Campaign, CampaignCreateRequest>().ReverseMap();
            CreateMap<Entity.Models.Campaign, CampaignUpdateRequest>().ReverseMap();
            CreateMap<Entity.Models.Campaign, CampaignResponse>().ReverseMap();
            CreateMap<Entity.Models.Campaign, CampaignDetailResponse>().ReverseMap();


            //CampaignDetail
            CreateMap<CampaignDetail, CampaignPriceCreateRequest>().ReverseMap();
            CreateMap<CampaignDetail, CampaignPriceUpdateRequest>().ReverseMap();
            CreateMap<CampaignDetail, CampaignPriceResponse>().ReverseMap();

            //Order
            CreateMap<Entity.Models.Order, OrderCreateViewModel>().ReverseMap();
            CreateMap<Entity.Models.Order, OrderUpdateViewModel>().ReverseMap();
            CreateMap<Entity.Models.Order, OrderViewModel>().ReverseMap();

            //OrderItem
            CreateMap<Entity.Models.OrderItem, OrderItemCreateViewModel>().ReverseMap();
            CreateMap<Entity.Models.OrderItem, OrderItemUpdateViewModel>().ReverseMap();
            CreateMap<Entity.Models.OrderItem, OrderItemViewModel>().ReverseMap();

            //Payment
            CreateMap<Entity.Models.Payment, PaymentCreateViewModel>().ReverseMap();
            CreateMap<Entity.Models.Payment, PaymentUpdateViewModel>().ReverseMap();
            CreateMap<Entity.Models.Payment, PaymentViewModel>().ReverseMap();

            // Add other mappings here as needed
        }
    }
}
