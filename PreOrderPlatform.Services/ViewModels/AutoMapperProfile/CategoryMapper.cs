using AutoMapper;
using PreOrderPlatform.Service.ViewModels.Category;
using PreOrderPlatform.Service.ViewModels.Category.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class CategoryMapper
    {
        public static void ConfigCategoryMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.Category, CategoryCreateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Category, CategoryUpdateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Category, CategoryViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Category, CategoryResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.Category, CategorySearchRequest>().ReverseMap();

        }
    }
}
