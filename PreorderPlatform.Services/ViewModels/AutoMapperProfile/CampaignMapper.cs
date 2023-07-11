using AutoMapper;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Campaign.Request;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class CampaignMapper
    {
        public static void ConfigCampaignMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignCreateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignUpdateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignSearchRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.Campaign, CampaignDetailResponse>().ReverseMap();
        }
    }
}
