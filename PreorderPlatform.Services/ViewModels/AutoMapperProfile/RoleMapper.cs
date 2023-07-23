using AutoMapper;
using PreOrderPlatform.Service.ViewModels.Role;

namespace PreOrderPlatform.Service.ViewModels.AutoMapperProfile
{
    public static class RoleMapper
    {
        public static void ConfigRoleMapper(this IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Entity.Models.Role, RoleCreateViewModel>().ReverseMap();
            configuration.CreateMap<Entity.Models.Role, RoleDetailViewModel>().ReverseMap();
        }
    }
}
