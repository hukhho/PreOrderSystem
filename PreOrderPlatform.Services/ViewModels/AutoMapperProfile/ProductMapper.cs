using AutoMapper;
using PreOrderPlatform.Service.ViewModels.Product.Request;
using PreOrderPlatform.Service.ViewModels.Product.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class ProductMapper
    {
        public static void ConfigProductMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.Product, ProductCreateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.Product, ProductUpdateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ReverseMap();
            configuration.CreateMap<Entity.Models.Product, ProductByIdResponse>();
            configuration.CreateMap<Entity.Models.Product, ProductSearchRequest>().ReverseMap();

        }
    }
}
