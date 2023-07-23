using AutoMapper;
using PreOrderPlatform.Service.ViewModels.OrderItem;
using PreOrderPlatform.Service.ViewModels.OrderItem.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class OrderItemMapper
    {
        public static void ConfigOrderItemMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.OrderItem, OrderItemCreateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.OrderItem, OrderItemUpdateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.OrderItem, OrderItemViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.OrderItem, OrderItemResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.OrderItem, OrderItemSearchRequest>().ReverseMap();

        }
    }
}
