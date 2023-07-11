using AutoMapper;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class CategoryMapper
    {
        public static void ConfigCategoryMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.Category, CategoryCreateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Category, CategoryUpdateViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Category, CategoryViewModel>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Category, CategorySearchRequest>().ReverseMap();

        }
    }
}
