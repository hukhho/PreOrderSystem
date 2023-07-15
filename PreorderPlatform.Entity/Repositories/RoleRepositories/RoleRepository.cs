using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.RoleRepositories
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
