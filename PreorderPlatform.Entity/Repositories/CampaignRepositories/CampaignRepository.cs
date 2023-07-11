using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.CampaignRepositories
{
    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {
        public CampaignRepository(PreOrderSystemContext context) : base(context)
        {

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
             u => u.Include(c => c.Owner),
             u => u.Include(c => c.CampaignDetails),
             u => u.Include(c => c.Product).ThenInclude(cd => cd.Category));

            return campaign;
        }


    }
}