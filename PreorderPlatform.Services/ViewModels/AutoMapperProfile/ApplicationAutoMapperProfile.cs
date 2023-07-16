using AutoMapper;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.BusinessPaymentCredential;
using PreorderPlatform.Service.ViewModels.Campaign.Request;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Response;
using PreorderPlatform.Service.ViewModels.Category;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.ViewModels.OrderItem;
using PreorderPlatform.Service.ViewModels.Payment;
using PreorderPlatform.Service.ViewModels.Product.Request;
using PreorderPlatform.Service.ViewModels.Product.Response;
using PreorderPlatform.Service.ViewModels.Role;
using PreorderPlatform.Service.ViewModels.User.Request;
using PreorderPlatform.Service.ViewModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            // User mappings
            CreateMap<PreorderPlatform.Entity.Models.User, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business.Name))
                .ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.User, UserUpdateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.User, UserCreateRequest>().ReverseMap();

            //Role mappings
            CreateMap<PreorderPlatform.Entity.Models.Role, RoleCreateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Role, RoleDetailViewModel>().ReverseMap();

            //Category
            CreateMap<PreorderPlatform.Entity.Models.Category, CategoryCreateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Category, CategoryUpdateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Category, CategoryViewModel>().ReverseMap();

            //Product
            CreateMap<PreorderPlatform.Entity.Models.Product, ProductCreateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Product, ProductUpdateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Product, ProductResponse>()
                                                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                                                .ReverseMap();

            //Business
            CreateMap<PreorderPlatform.Entity.Models.Business, BusinessCreateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Business, BusinessUpdateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Business, BusinessResponse>().ReverseMap();

            //BussinessPaymentCrential
            CreateMap<PreorderPlatform.Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialCreateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialUpdateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialViewModel>().ReverseMap();

            //Campaign
            CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignCreateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignUpdateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignResponse>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignDetailResponse>().ReverseMap();


            //CampaignDetail
            CreateMap<PreorderPlatform.Entity.Models.CampaignDetail, CampaignPriceCreateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.CampaignDetail, CampaignPriceUpdateRequest>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.CampaignDetail, CampaignPriceResponse>().ReverseMap();

            //Order
            CreateMap<PreorderPlatform.Entity.Models.Order, OrderCreateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Order, OrderUpdateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Order, OrderViewModel>().ReverseMap();

            //OrderItem
            CreateMap<PreorderPlatform.Entity.Models.OrderItem, OrderItemCreateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.OrderItem, OrderItemUpdateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.OrderItem, OrderItemViewModel>().ReverseMap();

            //Payment
            CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentCreateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentUpdateViewModel>().ReverseMap();
            CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentViewModel>().ReverseMap();

            // Add other mappings here as needed
        }
    }
}
