using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Immutable;

namespace PreorderPlatform.Entity.Repositories.BusinessRepositories
{
    public class BusinessRepository : RepositoryBase<Business>, IBusinessRepository
    {
        private readonly PreOrderSystemContext _context;

        public BusinessRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
        }

        // Add any additional methods specific to BusinessRepository here...
        public async Task<Business> GetBusinessByIdAsync(Guid id)
        {
            var businesses = await GetWithIncludeAsync(
                b => b.Id == id,
                b => b.Include(o => o.Owner!),
                b => b.Include(bp => bp.BusinessPaymentCredentials),
                b => b.Include(c => c.Campaigns),
                b => b.Include(p => p.Products).ThenInclude(c => c.Category!),
                b => b.Include(u => u.Users).ThenInclude(r => r.Role!)
                );

            //if (businesses != null)
            //{
            //    businesses.Users = businesses.Users.Where(user => user.RoleId != 2).ToList();
            //}
            return businesses;
        }
        public async Task<bool> IsUserOwnerOfBusiness(Guid userId, Guid businessId)
        {
            var business = await _context.Businesses
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == businessId && b.OwnerId == userId);

            return business != null;
        }

        public async Task<Business> GetByOwnerIdAsync(Guid userId)
        {
            var business = await GetWithIncludeAsync(
                b => b.OwnerId == userId,
                b => b.Include(o => o.Owner!),
                b => b.Include(bp => bp.BusinessPaymentCredentials),
                b => b.Include(c => c.Campaigns),
                b => b.Include(p => p.Products).ThenInclude(c => c.Category!),
                b => b.Include(u => u.Users).ThenInclude(r => r.Role!)
            );

            return business;
        }

    }
}