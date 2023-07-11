using AutoMapper;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Product.Request;
using PreorderPlatform.Service.ViewModels.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class ProductMapper
    {
        public static void ConfigProductMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.Product, ProductCreateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Product, ProductUpdateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Product, ProductByIdResponse>();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Product, ProductSearchRequest>().ReverseMap();

        }
    }
}
