using AutoMapper;
using PreOrderPlatform.Service.ViewModels.Order;
using PreOrderPlatform.Service.ViewModels.Order.Request;
using PreOrderPlatform.Service.ViewModels.Order.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class OrderMapper
    {
        public static void ConfigOrderMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.Order, OrderCreateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Order, OrderUpdateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Order, OrderViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Order, OrderResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.Order, OrderByIdResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.Order, OrderSearchRequest>().ReverseMap();

        }
    }
}
