using AutoMapper;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.ViewModels.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class OrderItemMapper
    {
        public static void ConfigOrderItemMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.OrderItem, OrderItemCreateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.OrderItem, OrderItemUpdateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.OrderItem, OrderItemViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.OrderItem, OrderItemSearchRequest>().ReverseMap();

        }
    }
}
