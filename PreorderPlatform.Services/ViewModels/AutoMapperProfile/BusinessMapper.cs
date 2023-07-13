using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class BusinessMapper
    {
        public static void ConfigBusinessMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.Business, BusinessCreateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Business, BusinessUpdateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Business, BusinessResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Business, BusinessByIdResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Business, BusinessPatchRequest>().ReverseMap();
          
            configuration.CreateMap<BusinessPatchRequest, BusinessUpdateRequest>();

            configuration.CreateMap<BusinessByIdResponse, BusinessUpdateRequest>();
            configuration.CreateMap<BusinessUpdateRequest, BusinessByIdResponse>();

            configuration.CreateMap<BusinessByIdResponse, BusinessPatchRequest>();
            configuration.CreateMap<BusinessPatchRequest, BusinessByIdResponse>();
        }
    }
}
