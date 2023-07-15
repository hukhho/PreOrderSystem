using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.ViewModels.Role;

namespace PreorderPlatform.Service.Services.RoleServices
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
