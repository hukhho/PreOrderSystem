using PreOrderPlatform.Service.ViewModels.Role;

namespace PreOrderPlatform.Service.Services.RoleServices
{
    public interface IRoleService
    {
        Task<List<RoleDetailViewModel>> GetRolesAsync();

        Task<RoleDetailViewModel> GetRoleByIdAsync(Guid id);

        Task<RoleDetailViewModel> CreateRoleAsync(RoleCreateViewModel model);

        Task UpdateRoleAsync(RoleDetailViewModel model);

        Task DeleteRoleAsync(Guid id);
    }
}
