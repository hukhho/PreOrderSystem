using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;
using System.Collections.Immutable;

namespace PreorderPlatform.Entity.Repositories.BusinessRepositories
{
    public class BusinessRepository : RepositoryBase<Business>, IBusinessRepository
    {
        public BusinessRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to BusinessRepository here...
        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            var businesses = await GetWithIncludeAsync(
                b => b.Id == id,
                b => b.Include(bp => bp.BusinessPaymentCredentials),
                b => b.Include(c => c.Campaigns),
                b => b.Include(p => p.Products).ThenInclude(c => c.Category),
                b => b.Include(u => u.Users).ThenInclude(r => r.Role)
                );

            if (businesses != null)
            {
                businesses.Users = businesses.Users.Where(user => user.RoleId != 2).ToList();
            }
            return businesses;
        }
    }
}