using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.CampaignRepositories
{
    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {
        private readonly PreOrderSystemContext _context;

        public CampaignRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
        }

        // Add any additional methods specific to CampaignRepository here...
        public async Task<IEnumerable<Campaign>> GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync()
        {
            return await GetAllWithIncludeAsync(u => true, u => u.Product, u => u.Business, u => u.Owner, u => u.CampaignDetails);

           // return await GetAllWithIncludeLoadRelatedEntitiesAsync(u => true);
        }


        public async Task<Campaign> GetCampaignWithDetailsAsync(Guid id)
        {
            var campaign = await GetWithIncludeAsync(
             u => u.Id == id,
             u => u.Include(c => c.Business),
             u => u.Include(c => c.Owner).ThenInclude(r => r.Role),
             u => u.Include(c => c.CampaignDetails),
             u => u.Include(c => c.Product).ThenInclude(cd => cd.Category));

            return campaign;
        }

        public async Task<bool> IsOwnerOrStaff(Guid userId, Guid campaignId)
        {
            var campaign = await _context.Campaigns.FindAsync(campaignId);

            if (campaign == null)
            {
                return false;
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.BusinessId == campaign.BusinessId && u.Id == userId);

            return user != null;
        }
    }
}