using AutoMapper;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.ViewModels.Order.Request;
using PreorderPlatform.Service.ViewModels.Order.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class OrderMapper
    {
        public static void ConfigOrderMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.Order, OrderCreateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Order, OrderUpdateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Order, OrderViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Order, OrderResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Order, OrderByIdResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Order, OrderSearchRequest>().ReverseMap();

        }
    }
}
