using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.RoleRepositories
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<Role> GetByNameAsync(string roleName);
    }
}
