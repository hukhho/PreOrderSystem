using AutoMapper;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Payment;
using PreorderPlatform.Service.ViewModels.Payment.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class PaymentMapper
    {
        public static void ConfigPaymentMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentCreateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentUpdateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentByIdResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Payment, PaymentSearchRequest>().ReverseMap();

        }
    }
}
