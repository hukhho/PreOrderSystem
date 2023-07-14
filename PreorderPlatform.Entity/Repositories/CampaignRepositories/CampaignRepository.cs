using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.CampaignRepositories
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


        public async Task<Campaign> GetCampaignWithDetailsAsync(int id)
        {
            var campaign = await GetWithIncludeAsync(
             u => u.Id == id,
             u => u.Include(c => c.Business),
             u => u.Include(c => c.Owner).ThenInclude(r => r.Role),
             u => u.Include(c => c.CampaignDetails),
             u => u.Include(c => c.Product).ThenInclude(cd => cd.Category));

            return campaign;
        }

        public async Task<bool> IsOwnerOrStaff(int userId, int campaignId)
        {
            bool res = false;
            var searchCampaign = _context.Campaigns.Find(campaignId);
            if (searchCampaign != null)
            {
                var businessId = searchCampaign.BusinessId;
                var listUser = await _context.Users.Where(c => c.BusinessId == businessId).ToListAsync();
                foreach (var user in listUser)
                {
                    if ((user.RoleId == 2 || user.RoleId == 3) && user.Id == userId)
                    {
                        res = true;
                    }
                }
            }

            return res;
        }
    }
}