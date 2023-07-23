using AutoMapper;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.ViewModels.Campaign.CampaignImages;
using PreOrderPlatform.Service.ViewModels.Campaign.Request;
using PreOrderPlatform.Service.ViewModels.Campaign.Response;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class CampaignMapper
    {
        public static void ConfigCampaignMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.Campaign, CampaignCreateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.Campaign, CampaignUpdateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.Campaign, CampaignResponse>().ReverseMap();
            configuration.CreateMap<Entity.Models.Campaign, CampaignSearchRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.Campaign, CampaignDetailResponse>()
                                                                 .ForMember(dest => dest.CampaignDetail, opt => opt.MapFrom(src => src.CampaignDetails))
                                                                 .ForMember(dest => dest.CampaignImages, opt => opt.MapFrom(src => src.Images))
                                                                 .ReverseMap();
            configuration.CreateMap<Entity.Models.Campaign, ChangeCampaignStatusRequest>().ReverseMap();
            configuration.CreateMap<CampaignImage, CampaignImageCreate>().ReverseMap();
            configuration.CreateMap<CampaignImage, CampaignImageView>().ReverseMap();

        }
    }
}
