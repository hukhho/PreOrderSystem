using AutoMapper;
using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential;
using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class BusinessPaymentCredentialMapper
    {
        public static void ConfigBusinessPaymentCredentialMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialCreateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialUpdateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.BusinessPaymentCredential, BusinessPaymentCredentialSearchRequest>().ReverseMap();

        }
    }
}
