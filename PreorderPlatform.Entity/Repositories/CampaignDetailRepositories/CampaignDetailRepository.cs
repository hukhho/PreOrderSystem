using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.CampaignDetailRepositories
{
    public class CampaignDetailRepository : RepositoryBase<CampaignDetail>, ICampaignDetailRepository
    {
        public CampaignDetailRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to CampaignDetailRepository here...
        public async Task<IEnumerable<CampaignDetail>> GetAllCampainDetailsAsync()
        {
            return await GetAllAsync();
        }
    }
}