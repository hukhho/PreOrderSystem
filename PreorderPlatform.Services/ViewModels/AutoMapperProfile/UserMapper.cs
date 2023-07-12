using AutoMapper;
using PreorderPlatform.Service.ViewModels.User.Request;
using PreorderPlatform.Service.ViewModels.User.Response;

namespace PreorderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class UserMapper
    {
        public static void ConfigUserMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PreorderPlatform.Entity.Models.User, UserResponse>()
                         .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null))
                         .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business != null ? src.Business.Name : null))
                         .ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.User, UserByIdResponse>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.User, UserUpdateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.User, UserCreateRequest>().ReverseMap();
            configuration.CreateMap<PreorderPlatform.Entity.Models.User, UserSearchRequest>().ReverseMap();
        }
    }
}
