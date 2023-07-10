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
    }
}