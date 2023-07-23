using AutoMapper;
using PreOrderPlatform.Service.ViewModels.Payment;
using PreOrderPlatform.Service.ViewModels.Payment.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class PaymentMapper
    {
        public static void ConfigPaymentMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.Payment, PaymentCreateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Payment, PaymentUpdateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Payment, PaymentViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Payment, PaymentResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.Payment, PaymentSearchRequest>().ReverseMap();

        }
    }
}
