using AutoMapper;
using PreOrderPlatform.Service.ViewModels.User.Request;
using PreOrderPlatform.Service.ViewModels.User.Response;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class UserMapper
    {
        public static void ConfigUserMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.User, UserResponse>()
                         .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null))
                         .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business != null ? src.Business.Name : null))
                         .ReverseMap();
            configuration.CreateMap<Entity.Models.User, UserUpdateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.User, UserCreateRequest>().ReverseMap();
            configuration.CreateMap<Entity.Models.User, UserSearchRequest>().ReverseMap();


            //configuration.CreateMap<PreOrderPlatform.Entity.Models.User, UserFullDetails>()
            //             .ReverseMap();
        }
    }
}
