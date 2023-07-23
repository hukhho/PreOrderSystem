using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.RoleRepositories
{
    internal class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly PreOrderSystemContext _context;

        public RoleRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role> GetByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
