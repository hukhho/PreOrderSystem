using AutoMapper;
using PreOrderPlatform.Service.ViewModels.Business.Request;
using PreOrderPlatform.Service.ViewModels.Business.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class BusinessMapper
    {
        public static void ConfigBusinessMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.Business, BusinessCreateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.Business, BusinessUpdateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.Business, BusinessResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.Business, BusinessByIdResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.Business, BusinessPatchRequest>().ReverseMap();
          
            configuration.CreateMap<BusinessPatchRequest, BusinessUpdateRequest>();

            configuration.CreateMap<BusinessByIdResponse, BusinessUpdateRequest>();
            configuration.CreateMap<BusinessUpdateRequest, BusinessByIdResponse>();

            configuration.CreateMap<BusinessByIdResponse, BusinessPatchRequest>();
            configuration.CreateMap<BusinessPatchRequest, BusinessByIdResponse>();
        }
    }
}
