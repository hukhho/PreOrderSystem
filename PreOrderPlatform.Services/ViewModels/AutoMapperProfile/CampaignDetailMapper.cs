using AutoMapper;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class CampaignDetailMapper
    {
        public static void ConfigCampaignDetailMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CampaignDetail, CampaignPriceCreateRequest>().ReverseMap();
            configuration.CreateMap<CampaignDetail, CampaignPriceUpdateRequest>().ReverseMap();
            configuration.CreateMap<CampaignDetail, CampaignPriceResponse>().ReverseMap();
            configuration.CreateMap<CampaignDetail, CampaignDetailSearchRequest>().ReverseMap();

        }
    }
}
